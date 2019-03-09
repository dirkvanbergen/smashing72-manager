define(function (require) {
    var Vue = require("vue.min");

    var form = require("forms/user-form");

    var comp = Vue.component("user-list",
        {
            template: "#userlist-template",
            data: function () {
                return {
                    greeting: "Hallo volleyballer"
                };
            }
        });

    return comp;
});