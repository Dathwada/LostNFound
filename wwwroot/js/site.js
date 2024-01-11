// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$("#dropdownCategory").select2({
    theme: 'bootstrap-5',
    placeholder: "Kategorie auswählen",
    allowClear: true
});

function filterCards() {
    // Remove class d-none from all cards
    // if the dropdown is empty.
    if ($("#dropdownCategory").val() === "") {
        $('.card').each(function () {
            $(this).removeClass('d-none');
        });
        $("#noEntries").addClass("d-none")
        return;
    };

    // Add class d-none for cards that not match
    // the category in the dropdown else remove it.
    $('.card').each(function () {
        if ($(this).find('.card-title').text() != $("#dropdownCategory").val()) {
            $(this).addClass('d-none');
        } else {
            $(this).removeClass('d-none');
        }
    });

    // Show h1 "Keine Einträge gefunden" if all cards have the class d-none.
    var areAllCardsHidden = $('.card').length === $('.card.d-none').length;
    areAllCardsHidden ? $("#noEntries").removeClass("d-none") : $("#noEntries").addClass("d-none");
}