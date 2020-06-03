function showPerson() {
    document.getElementById("PricePlan").innerHTML =
        `<div class="wizard-header">
                            <h3 class="heading">Person Pricing Plan</h3>
                            <p>Please select your pricing plan and proceed to the next step so we can build your
                                account.</p>
                        </div>
                        <div class="form-row">
                            <div class="columns">
                                <ul class="price">
                                    <li class="header">Basic</li>
                                    <li class="grey"> Free </li>
                                    <li class="pragraphe">Limited experiences</li>
                                    <li class="grey">
                                       <div class="radio-toolbar">
                                                    <input type="radio" id="SelectFree" name="IsPremiumAccount" checked value="False ">
                                                    <label for="SelectFree">Select</label>
                                       </div>
                                    </li>
                                </ul>
                            </div>

                            <div class="columns">
                                <ul class="price">
                                    <li class="header">Premium</li>
                                    <li class="grey"> 99 L.E </li>
                                    <li class="pragraphe">Unlimited experiences</li>
                                    <li class="grey">
                                        <div class="radio-toolbar">
                                                    <input type="radio" id="SelectPremium" name="IsPremiumAccount" value="True" onclick="PersonPremium()">
                                                    <label for="SelectPremium">Select</label>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>`;
    $("#SchoolSection").removeClass("d-block");
    $("#SchoolSection").addClass("d-none");
    document.getElementById("SchoolSection").innerHTML = ``;
}

function showSchool() {
    document.getElementById("PricePlan").innerHTML =
        `<div class="wizard-header">
                            <h3 class="heading">School Pricing Plan</h3>
                            <p>Please select your pricing plan and proceed to the next step so we can build your
                                account.</p>
                        </div>
                        <div class="form-row">
                            <div class="columns">
                                <ul class="price">
                                    <li class="header">Basic</li>
                                    <li class="grey"> Free </li>
                                    <li class="pragraphe">Limited experiences</li>
                                    <li class="pragraphe">15 students</li>
                                    <li class="grey">
                                        <div class="radio-toolbar">
                                               <input type="radio" id="SelectFree" name="IsPremiumAccount" checked value="False">
                                               <label for="SelectFree">Select</label>
                                        </div>
                                    </li>
                                </ul>
                            </div>

                            <div class="columns">
                                <ul class="price">
                                    <li class="header">Premium</li>
                                    <li class="grey"> 999 L.E </li>
                                    <li class="pragraphe">Unlimited experiences</li>
                                    <li class="pragraphe">Unlimited students</li>
                                    <li class="grey">
                                        <div class="radio-toolbar">
                                            <input type="radio" id="SelectPremium" name="IsPremiumAccount" value="True" onclick="SchoolPremium()">
                                             <label for="SelectPremium">Select</label>
                                        </div>
                                    </li>
                                </ul>
                            </div>
                        </div>`;

    $("#SchoolSection").removeClass("d-none");
    $("#SchoolSection").addClass("d-block");

    document.getElementById("SchoolSection").innerHTML = `<div class="form-holder form-holder-2">
                                            <fieldset>
                                                <legend>School Name</legend>
                                                <input class="form-control" type="text" name="SchoolName" id="SchoolName"
                                                       onblur="CheckSchoolName()" placeholder="School Name"
                                                       autocomplete="off" />
                                            </fieldset>
                                              <span class="field-validation-valid text-danger StatusSchoolName" data-valmsg-for="SchoolName" data-valmsg-replace="true">
                                              </span>
                                        </div>`;
}