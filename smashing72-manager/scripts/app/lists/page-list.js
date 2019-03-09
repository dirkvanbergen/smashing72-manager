define(function (require) {
    var Vue = require("vue.min");

    var form = require("forms/page-form");

    var comp = Vue.component("page-list",
        {
            template: "#pagelist-template",
            data: function () {
                return {
                    greeting: "Hallo volleyballer"
                };
            }
        });

    return comp;
});