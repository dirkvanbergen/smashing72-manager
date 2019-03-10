define(function (require) {
    var Vue = require("vue.min");

    var form = require("forms/news-form");

    var comp = Vue.component("news-list",
        {
            template: "#newslist-template",
            data: function () {
                return {
                    list: [],
                    selectedItem: null,
                    selectedYear: new Date().getFullYear(),
                    selectedMonth: new Date().getMonth() + 1
                };
            },
            computed: {
                years: function() {
                    return this.list.reduce(function(acc, item) {
                        if (acc.indexOf(item.Year) < 0) {
                            acc.push(item.Year);
                        }
                        return acc;
                    }, []).sort().reverse();
                }
            },
            methods: {
                selectYear: function(year) {
                    this.selectedYear = year;
                    this.selectedMonth = 0;
                },
                selectMonth: function(month) {
                    this.selectedMonth = parseInt(month.substring(0, 2));
                },
                getMonths: function (year) {
                    return this.list.reduce(function (acc, item) {
                        if (item.Year === year && acc.filter(function (i) { return i.Month === item.Month; }).length === 0) {
                            acc.push({ MonthName: item.MonthName, Month: item.Month });
                        }
                        return acc;
                    },
                    []).sort();
                },
                newItem: function () {
                    return {
                        Id: 0,
                        Title: '',
                        Article: '',
                        AuthorId: 1,
                        PublishDate: getDate(),
                        Day: getDate().getDate(),
                        Month: getDate().getMonth() + 1,
                        Year: getDate().getFullYear()
                    };
                },
                selectItem: function (item) {
                    this.selectedItem = item;
                },
                loadList: function () {
                    var self = this;
                    var http = new XMLHttpRequest();
                    var url = "/api/news/";

                    http.addEventListener("load", function (data) {
                        var news = data.currentTarget.response;
                        // empty
                        self.list.splice(0, self.list.length);
                        for (var i = 0; i < news.length; i++) {
                            self.list.push(news[i]);
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
                    var self = this;
                    var http = new XMLHttpRequest();
                    var url = "/api/news/" + this.selectedItem.Id;

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