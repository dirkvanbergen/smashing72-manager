define(function (require) {
    var Vue = require("vue.min");

    var comp = Vue.component("section-list",
        {
            template: "#sectionlist-template",
            props: ["section"],
            methods: {
                changeSection: function (section) {
                    this.$emit("section-changed", section);
                }
            }
        });

    return comp;
});