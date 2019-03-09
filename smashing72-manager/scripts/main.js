requirejs.config({
    baseUrl: "/scripts/lib",
    paths: {
        app: "/scripts/app",
        lists: "/scripts/app/lists",
        forms: "/scripts/app/forms"
    }
});

requirejs(["app/app"]);