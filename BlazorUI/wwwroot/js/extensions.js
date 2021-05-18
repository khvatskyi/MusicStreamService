function setUserToken(name, value, days) {
    let expires;
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    }
    else {
        expires = "";
    }

    if (!value) value = "";

    document.cookie = name + "=" + value + expires + "; path=/";
}

function romoveUserToken(name) {
    setUserToken(name);
}