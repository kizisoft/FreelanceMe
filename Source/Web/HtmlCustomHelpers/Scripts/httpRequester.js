var HtmlCustomHelpers = HtmlCustomHelpers || {};

(function (Scope) {
    var DEFAULT_TIMEOUT = 5000;

    function HttpRequester() {
        return {
            get: getJSON,
            post: postJSON
        };
    }

    function makeHttpRequest(verb, url, data) {
        var requestObject = {
            url: url,
            type: verb,
            contentType: 'application/json',
            timeout: DEFAULT_TIMEOUT
        };

        if (data) {
            requestObject.data = JSON.stringify(data);
        }

        return $.ajax(requestObject);
    }

    function getJSON(url) {
        return makeHttpRequest('GET', url);
    }

    function postJSON(url, data) {
        return makeHttpRequest('POST', url, data);
    }

    Scope.HttpRequester = new HttpRequester();
}(HtmlCustomHelpers));