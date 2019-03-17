define(function (require) {
    var Vue = require("vue.min");

    var comp = Vue.component("section-list",
        {
            template: "#sectionlist-template",
            props: ["section"],
            methods: {
                changeSection: function (section) {
                    this.$emit("section-changed", section);
                },
                logOut: function() {
                    var xhr = new XMLHttpRequest();
                    xhr.open("POST", "/Account/LogOut");


                    xhr.addEventListener("load", function (event) {
                        location.reload();
                    });

                    xhr.send();
                }
            }
        });

    return comp;
});