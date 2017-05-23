var zoo = {};

zoo.urls = {};

zoo.urls.animalsController = function () {
    return combinePath(zoo.baseUrl, "Animals");
};

zoo.urls.speciesController = function () {
    return combinePath(zoo.baseUrl, "Species");
};

function combinePath(baseUrl, relativeUrl) {
    var url = trimChar(baseUrl, "/") + "/" + trimChar(relativeUrl, "/");

    return url;
}

function trimChar(string, charToRemove) {
    while (string.charAt(0) == charToRemove) {
        string = string.substring(1);
    }

    while (string.charAt(string.length - 1) == charToRemove) {
        string = string.substring(0, string.length - 1);
    }

    return string;
}