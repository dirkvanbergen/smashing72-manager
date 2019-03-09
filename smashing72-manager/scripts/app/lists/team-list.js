define(function (require) {
    var Vue = require("vue.min");

    var form = require("forms/team-form");

    var comp = Vue.component("team-list",
        {
            template: "#teamlist-template",
            data: function () {
                return {
                    greeting: "Hallo volleyballer"
                };
            }
        });

    return comp;
});