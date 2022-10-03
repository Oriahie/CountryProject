// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function preloadLanguages() {
    if (!localStorage.getItem('supportedLang')) {
        localStorage.setItem('supportedLang', 'English,German');
    }
    var English = {
        homeOption: "Home",
        countryOption: "Countries",
        homePage: "Welcome To Country Project",
        countryListName: "List Of Countries",
        countryDetailsName: "Country Details",
        neighboringCountryName: "Neighbouring Countries"
    }
    if (!localStorage.getItem('English')) {
        localStorage.setItem('English', JSON.stringify(English));
    }
    var German = {
        homeOption: "Heim",
        countryOption: "Länder",
        homePage: "Willkommen beim Länderprojekt",
        countryListName: "Liste der Länder",
        countryDetailsName: "Länderdetails",
        neighboringCountryName: "Nachbarländer"
    }
    if (!localStorage.getItem('German')) {
        localStorage.setItem('German', JSON.stringify(German));
    }

}
preloadLanguages();


var supportedLang = localStorage.getItem("supportedLang");
if (supportedLang != null) {
    var list = supportedLang.split(',');
    $('#culture-picker').empty();

    $('#culture-picker').append('<option selected disabled>Select Language</option>');
    for (var i = 0; i < list.length; i++) {
        var val = $(`<option value="${list[i]}">${list[i]}</option>`);
        $('#culture-picker').append(val);
    }

}

var chosenLang = localStorage.getItem("chosenLang");
if (chosenLang) {
    var lng = localStorage.getItem(chosenLang);
    if (lng) {
        var val = JSON.parse(lng);
        localize(val);
    }
}


function localizelanguage() {
    var lng = $('#culture-picker').val();

    //save chosen language
    localStorage.setItem("chosenLang", lng);

    var chosenLang = localStorage.getItem(lng);

    if (chosenLang) {
        let val = JSON.parse(chosenLang);
        localize(val);
    }

}

function localize(data) {
    $('#homeOption').text(data.homeOption)
    $('#countryOption').text(data.countryOption)
    $('#homePage').text(data.homePage)
    $('#countryListName').text(data.countryListName)
    $('#countryDetailsName').text(data.countryDetailsName)
    $('#neighboringCountryName').text(data.neighboringCountryName)
}