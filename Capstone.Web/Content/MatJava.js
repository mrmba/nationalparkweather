var activeButtons = function () {
    var calsses = document.querySelector("#navHomeButton").className;
    classes = classes.replace(new RedExp("active", "g"), "");
    document.querySelector("#navHomeButton").className = classes;

    classes = document.querySelector("#navMenuButton").className;
    if (classes.indexOf("active") == -1) {
        classes += " active";
        document.querySelector("#navMenuButton").className = classes;
    }
}