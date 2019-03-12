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
                    this.selectedYear = (this.selectedYear === year ? 0 : year);
                    this.selectedMonth = 0;
                    this.selectedItem = null;
                },
                selectMonth: function (month) {
                    this.selectedMonth = (this.selectedMonth === month.Month ? 0 : month.Month);
                    this.selectedItem = null;
                },
                getMonths: function (year) {
                    return this.list.reduce(function (acc, item) {
                        if (item.Year === year && acc.filter(function (i) { return i.Month === item.Month; }).length === 0) {
                            acc.push({ MonthName: item.MonthName, Month: item.Month });
                        }
                        return acc;
                    },
                        []).sort(function (a, b) { return b.Month - a.Month; });
                },
                getItems: function(year, month) {
                    return this.list.filter(function(item) {
                        return item.Year === year && item.Month === month.Month;
                    }).sort(function (a, b) { return b.Id - a.Id; });
                },
                newItem: function () {
                    var today = new Date();
                    return {
                        Id: 0,
                        Title: '',
                        Article: '',
                        AuthorId: 1,
                        Day: today.getDate(),
                        Month: today.getMonth() + 1,
                        Year: today.getFullYear()
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
                    if (this.selectedItem.Id === 0) return;
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