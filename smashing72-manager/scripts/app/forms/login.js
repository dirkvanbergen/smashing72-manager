define(function (require) {
    var Vue = require("vue.min");

    var comp = Vue.component("login",
        {
            template: "#login-template",
            data: function () {
                return {
                    currentAction: "login"
                };
            },
            computed: {
                buttonText: function() {
                    return this.currentAction === "login" ? "Inloggen" : "Registreren";
                },
                formAction: function () {
                    return this.currentAction === "login" ? "/Account/Login" : "/Account/Register";
                },
                formTitle: function() {
                    return this.currentAction === "login" ? "Log in" : "Registreer";
                }
            },
            methods: {
                sendForm: function () {
                    var self = this;
                    var form = this.$refs["form"];
                    var XHR = new XMLHttpRequest();

                    // Bind the FormData object and the form element
                    var FD = new FormData(form);

                    // Define what happens on successful data submission
                    XHR.addEventListener("load", function (event) {
                        var result = JSON.parse(event.target.responseText);
                        if (result.success) {
                            location.reload();
                        } else {
                            self.loginFailed = true;
                        }
                    });

                    // Define what happens in case of error
                    XHR.addEventListener("error", function (event) {
                        alert('Oops! Something went wrong.');
                    });

                    // Set up our request
                    XHR.open("POST", this.formAction);

                    // The data sent is what the user provided in the form
                    XHR.send(FD);

                }
            }
        });

    return comp;
});