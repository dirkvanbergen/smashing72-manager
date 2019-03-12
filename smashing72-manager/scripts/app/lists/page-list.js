define(function (require) {
    var Vue = require("vue.min");

    var form = require("forms/page-form");

    var comp = Vue.component("page-list",
        {
            template: "#pagelist-template",
            data: function () {
                return {
                    list: [],
                    selectedItem: null
                };
            },
            computed: {
                topLevelItems: function () {
                    var items = this.list.filter(function (item) {
                        return !item.ParentContentId;
                    });
                    items.sort(function (a, b) { return a.MenuOrder - b.MenuOrder; });
                    return items;
                },
                potentialParents: function () {
                    return this.topLevelItems.map(function (i) {
                        return { Id: i.Id, Name: i.MenuTitle, UrlSegment: i.UrlSegment };
                    });
                }
            },
            methods: {
                getChildren: function (item) {
                    var items = this.list.filter(function (sub) {
                        return sub.ParentContentId === item.Id;
                    });
                    items.sort(function (a, b) { return a.MenuOrder - b.MenuOrder; });
                    return items;
                },
                newItem: function () {
                    return {
                        Id: 0,
                        MenuTitle: '',
                        Title: '',
                        Html: '',
                        MenuOrder: 0,
                        ShowInMenu: true,
                        DataType: 'ContentPage',
                        UrlSegment: '',
                        ParentContentId: null
                    };
                },
                selectItem: function (item) {
                    this.selectedItem = item;
                },
                loadList: function () {
                    var self = this;
                    var http = new XMLHttpRequest();
                    var url = "/api/Page/";

                    http.addEventListener("load", function (data) {
                        var pages = data.currentTarget.response;
                        // empty
                        self.list.splice(0, self.list.length);
                        for (var i = 0; i < pages.length; i++) {
                            self.list.push(pages[i]);
                        }
                    });
                    http.open("GET", url);
                    http.responseType = "json";
                    http.send();
                },
                itemAdded: function (item) {
                    this.list.push(item);
                    this.selectedItem = null;
                },
                itemUpdated: function () {
                    this.selectedItem = null;
                },
                itemDeleted: function (item) {
                    var index = -1;
                    for (var i = 0; i < this.list.length; i++) {
                        if (this.list[i].Id === item.Id) {
                            index = i;
                            break;
                        }
                    }
                    if (index >= 0) {
                        this.list.splice(index, 1);
                    }
                    this.selectedItem = null;
                },
                formClosed: function () {
                    if (this.selectedItem.Id === 0) return;
                    var self = this;
                    var http = new XMLHttpRequest();
                    var url = "/api/Page/" + this.selectedItem.Id;

                    http.addEventListener("load", function (data) {
                        var item = data.currentTarget.response;
                        var index = -1;
                        for (var i = 0; i < self.list.length; i++) {
                            if (self.list[i].Id === item.Id) {
                                index = i;
                                break;
                            }
                        }
                        if (index >= 0) self.list.splice(index, 1, item);
                    });
                    http.open("GET", url);
                    http.responseType = "json";
                    http.send();
                    this.selectedItem = null;
                },
                closeList: function() {
                    this.$emit("list-closed");
                }
            }
        });

    return comp;
});