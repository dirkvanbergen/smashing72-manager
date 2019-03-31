define(function (require) {
    var Vue = require("vue.min");
    
    var comp = Vue.component("page-form",
        {
            template: "#pageform-template",
            props: ["item", "parents"],
            components: {
                'editor': Editor
            },
            data: function() {
                return {
                    tinyInit: {
                        height: "500px",
                        width: "100%",
                        contextmenu: "link image imagetools table",
                        plugins: "searchreplace autolink code visualblocks visualchars image link media table advlist lists wordcount imagetools textpattern",
                        toolbar: "formatselect | bold italic strikethrough forecolor backcolor | link image media | alignleft aligncenter alignright alignjustify  | numlist bullist | removeformat",
                        images_upload_url: "/api/image",
                        automatic_uploads: true,
                        images_reuse_filename: true
                    }
                };
            },
            computed: {
                finalUrl: function () {
                    var self = this;
                    var url = "https://smashing72.nl/";

                    if (this.item.ParentContentId) {
                        var parents = this.parents.filter(function (p) { return p.Id === self.item.ParentContentId; });
                        if (parents.length === 1) url += parents[0].UrlSegment + "/";
                    }

                    url += (this.item.UrlSegment + "/").replace("//", "/");

                    url = url.replace(".nl//", ".nl/");

                    return url;
                }
            },
            methods: {
                savePage: function () {
                    var self = this;
                    var http = new XMLHttpRequest();
                    var url = "/api/page/";
                    url += this.item.Id > 0 ? "update/" + this.item.Id : "add/";

                    http.addEventListener("load", function (e) {
                        var response = e.currentTarget.response;
                        if (response) {
                            self.$emit("item-added", response);
                        } else {
                            self.$emit("item-updated");
                        }
                    });

                    if (this.item.ParentContentId === "") this.item.ParentContentId = null;

                    http.open("POST", url);

                    http.setRequestHeader("Content-Type", "application/json");
                    http.responseType = "json";
                    http.send(JSON.stringify(this.item));
                }, 
                deletePage: function () {
                    if (this.item.Id === 0) return;
                    var self = this;
                    var http = new XMLHttpRequest();
                    var url = "/api/page/delete/" + this.item.Id;

                    http.addEventListener("load", function (e) {
                        var response = e.currentTarget.response;
                        self.$emit("item-deleted", response);
                    });
                    http.open("POST", url);

                    http.responseType = "json";
                    http.send();
                },
                closeForm: function() {
                    this.$emit("close");
                }
            }
        });

    return comp;
});