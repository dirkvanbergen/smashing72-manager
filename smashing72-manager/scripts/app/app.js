define(function (require) {

    var Vue = require("vue.min");

    var sectList = require("lists/section-list");
    var pageList = require("lists/page-list");
    var newsList = require("lists/news-list");
    var teamList = require("lists/team-list");
    var userList = require("lists/user-list");
    var login = require("forms/login");

    var app = new Vue({
        el: "#app",
        data: {
            currentSection: ""
        },
        methods: {
            getList: function(listType) {
                return this.$children.filter(function(c) {
                    return c.$el.id === listType + "-view";
                })[0];
            },
            loadList: function(listType) {
                this.currentSection = listType;
                this.getList(listType).loadList();
            }
        }
    });
});