requirejs.config({
    baseUrl: "/scripts/lib",
    paths: {
        app: "/scripts/app",
        lists: "/scripts/app/lists",
        forms: "/scripts/app/forms",
        tinymce: "/scripts/lib/tinymce"
    }
});

requirejs(["app/app"]);