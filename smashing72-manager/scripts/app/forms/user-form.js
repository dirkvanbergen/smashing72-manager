define(function (require) {
    var Vue = require("vue.min");

    var comp = Vue.component("user-form",
        {
            template: "#userform-template",
            props: ["item", "roles"],
            data: function () {
                return {

                };
            },
            computed: {
            },
            methods: {
                hasRole: function(role) {
                    return this.item.Roles.filter(function(r) { return r === role; }).length > 0;
                },
                toggleRole: function(role) {
                    if (this.hasRole(role)) {
                        this.removeRole(role);
                    } else {
                        this.addRole(role);
                    }
                },
                addRole: function (role) {
                    this.item.Roles.push(role);
                    var self = this;
                    var http = new XMLHttpRequest();
                    var url = "/api/user/addrole/" + this.item.Id + "?role=" + role;
                    http.open("POST", url);

                    http.responseType = "json";
                    http.send();
                },
                removeRole: function (role) {
                    this.item.Roles.splice(this.item.Roles.indexOf(role), 1);
                    var http = new XMLHttpRequest();
                    var url = "/api/user/removerole/" + this.item.Id + "?role=" + role;
                    http.open("POST", url);

                    http.responseType = "json";
                    http.send();
                },
                deleteUser: function () {
                    if (this.item.Id === 0) return;
                    var self = this;
                    var http = new XMLHttpRequest();
                    var url = "/api/user/delete/" + this.item.Id;

                    http.addEventListener("load", function (e) {
                        var response = e.currentTarget.response;
                        self.$emit("item-deleted", response);
                    });
                    http.open("POST", url);

                    http.responseType = "json";
                    http.send();
                },
                closeForm: function () {
                    this.$emit("close");
                }
            }
        });

    return comp;
});