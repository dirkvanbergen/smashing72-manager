define(function (require) {
    var Vue = require("vue.min");

    var form = require("forms/news-form");

    var comp = Vue.component("news-list",
        {
            template: "#newslist-template",
            data: function () {
                return {
                    greeting: "Hallo volleyballer"
                };
            }
        });

    return comp;
});