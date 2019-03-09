define(function (require) {

    var Vue = require("vue.min");

    var sectList = require("lists/section-list");
    var pageList = require("lists/page-list");
    var newsList = require("lists/news-list");
    var teamList = require("lists/team-list");
    var userList = require("lists/user-list");

    var app = new Vue({
        el: "#app",
        data: {
            listType: ""
        },
        methods: {
            loadList: function(listType) {
                this.listType = listType;
            }
        }
    });
});