define(function (require) {
    var Vue = require("vue.min");

    var form = require("forms/user-form");

    var comp = Vue.component("user-list",
        {
            template: "#userlist-template",
            data: function () {
                return {
                    list: [],
                    roles: [],
                    selectedItem: null
                };
            },
            methods: {
                loadList: function () {
                    var self = this;
                    var http = new XMLHttpRequest();
                    var url = "/api/user/";

                    http.addEventListener("load", function (data) {
                        var items = data.currentTarget.response;
                        // empty
                        self.list.splice(0, self.list.length);
                        for (var i = 0; i < items.length; i++) {
                            self.list.push(items[i]);
                        }
                        self.loadRoles();
                    });
                    http.open("GET", url);
                    http.responseType = "json";
                    http.send();
                },
                loadRoles: function () {
                    var self = this;
                    var http = new XMLHttpRequest();
                    var url = "/api/role/";

                    http.addEventListener("load", function (data) {
                        var items = data.currentTarget.response;
                        // empty
                        self.roles.splice(0, self.roles.length);
                        for (var i = 0; i < items.length; i++) {
                            self.roles.push(items[i]);
                        }
                    });
                    http.open("GET", url);
                    http.responseType = "json";
                    http.send();
                },
                newItem: function() {
                    return {
                        Username: "",
                        Password: ""
                    };
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
                    var url = "/api/user/" + this.selectedItem.Id;

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
                closeList: function () {
                    this.$emit("list-closed");
                }
            }
        });

    return comp;
});