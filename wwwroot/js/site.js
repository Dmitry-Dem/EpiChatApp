var menu = document.getElementById("menu");
var mainBody = document.getElementById("main-body");

function openMenu() {
    if (menuIsClosed()) {
        menu.classList.remove("menu-close");

        //if menu closed, open menu and change main body width
        mainBody.classList.remove("new-main-body-width")
    }
    else {
        menu.classList.add("menu-close");

        //if menu open, close menu and change main body width
        mainBody.classList.add("new-main-body-width")
    }
}

function menuIsClosed() {
    return menu.classList.contains("menu-close");
}