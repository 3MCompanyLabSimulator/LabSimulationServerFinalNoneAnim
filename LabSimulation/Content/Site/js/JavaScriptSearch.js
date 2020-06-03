function myFunctionSearch() {
    var input, filter, DivSearchArea, ExperienceItem, H5, i, txtValue;
    input = document.getElementById("myInput");
    filter = input.value.toUpperCase();

    DivSearchArea = document.getElementById("SearchArea");

    ExperienceItem = DivSearchArea.getElementsByClassName("ExperienceSearch");


    for (i = 0; i < ExperienceItem.length; i++) {
        H5 = ExperienceItem[i].getElementsByTagName("h5")[0];
        txtValue = H5.textContent || H5.innerText;
        if (txtValue.toUpperCase().indexOf(filter) > -1) {
            ExperienceItem[i].style.display = "";
        } else {
            ExperienceItem[i].style.display = "none";
        }
    }
}