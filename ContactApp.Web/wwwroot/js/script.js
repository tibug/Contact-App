// Toggle filter options visibility
document.querySelectorAll('.filter-header').forEach(header => {
    header.addEventListener('click', () => {
        const options = header.nextElementSibling;
        const isActive = options.classList.contains('active');

        // Close all other filter sections
        document.querySelectorAll('.filter-options').forEach(option => {
            if (option !== options) {
                option.classList.remove('active');
            }
        });

        // Toggle the current filter section
        options.classList.toggle('active', !isActive);
    });
});

// Add filter when Enter key is pressed or input loses focus
document.querySelectorAll('.filter-option input').forEach(input => {
    input.addEventListener('keydown', function (e) {
        if (e.key === 'Enter') {
            addFilter(e.target);
        }
    });
    input.addEventListener('blur', function (e) {
        addFilter(e.target);
    });
});

document.querySelectorAll('.form-check-input').forEach(checkbox => {
    checkbox.addEventListener('change', function () {
        addFilter(checkbox);
    });
});

function addFilter(inputElement) {
    const value = inputElement.value.trim();
    // If the value is empty or just 'on', return (except for checkboxes)
    if (!value && inputElement.type !== 'checkbox') {
        return false;
    }
    const id = inputElement.id;
    const filterType = inputElement.closest('.filter-category').querySelector('.filter-header span').textContent.trim();
    const selectedFiltersContainer = inputElement.closest('.filter-category').querySelector('.selected-filters');

    // For checkboxes, the value to add is the checkbox's id, not its 'on' state
    let badgeValue;

    if (inputElement.type === 'checkbox') {
        // Find the label for the checkbox using the `for` attribute
        const label = document.querySelector(`label[for="${id}"]`);
        badgeValue = label ? label.textContent.trim() : '';
    } else {
        badgeValue = value;
    }

    // If the input is empty and not a checkbox, exit the function
    if (!badgeValue) {
        return false;
    }

    // Check if a filter badge already exists
    let filterBadge = selectedFiltersContainer.querySelector(`.filter-badge[data-value="${badgeValue}"]`);

    // Handle checkboxes (add filter badge if checked, remove if unchecked)
    if (inputElement.type === 'checkbox') {
        if (inputElement.checked && !filterBadge) {
            filterBadge = document.createElement('div');
            filterBadge.className = 'filter-badge';
            filterBadge.dataset.value = badgeValue;
            filterBadge.textContent = `${filterType}: ${badgeValue}`;
            const removeIcon = document.createElement('i');
            removeIcon.className = 'fas fa-times';
            removeIcon.addEventListener('click', () => {
                filterBadge.remove();
                updateModel(id);
            });
            filterBadge.appendChild(removeIcon);
            selectedFiltersContainer.appendChild(filterBadge);
            updateModel(id);
        } else if (!inputElement.checked && filterBadge) {
            filterBadge.remove();
            updateModel(id);
        }
    }
    // Handle text inputs (add filter badge when user presses Enter or search is clicked)
    else {
        if (!filterBadge && value) {
            filterBadge = document.createElement('div');
            filterBadge.className = 'filter-badge';
            filterBadge.dataset.value = badgeValue;
            filterBadge.textContent = `${filterType}: ${badgeValue}`;
            const removeIcon = document.createElement('i');
            removeIcon.className = 'fas fa-times';
            removeIcon.addEventListener('click', () => {
                filterBadge.remove();
                updateModel(id);
            });
            filterBadge.appendChild(removeIcon);
            selectedFiltersContainer.appendChild(filterBadge);
            inputElement.value = ''; // Clear the input after adding the filter
            updateModel(id);
        }
    }
}


//function addFilter(inputElement) {
//    const value = inputElement.value.trim();

//    // If the value is empty or just 'on', return (except for checkboxes)
//    if (!value && inputElement.type !== 'checkbox') {
//        return false;
//    }

//    const id = inputElement.id;
//    const filterType = inputElement.closest('.filter-category').querySelector('.filter-header span').textContent.trim();
//    const selectedFiltersContainer = inputElement.closest('.filter-category').querySelector('.selected-filters');

//    // Check if filter already exists
//    let filterBadge = selectedFiltersContainer.querySelector(`.filter-badge[data-value="${value}"]`);

//    // If checkbox, use inputElement.id as value for the filter badge
//    const badgeValue = inputElement.type === 'checkbox' ? inputElement.id : value;

//    if (badgeValue && !filterBadge && inputElement.checked) {
//        filterBadge = document.createElement('div');
//        filterBadge.className = 'filter-badge';
//        filterBadge.dataset.value = badgeValue;
//        filterBadge.textContent = `${filterType}: ${badgeValue}`;
//        const removeIcon = document.createElement('i');
//        removeIcon.className = 'fas fa-times';
//        removeIcon.addEventListener('click', () => {
//            filterBadge.remove();
//            updateModel(id);
//        });
//        filterBadge.appendChild(removeIcon);
//        selectedFiltersContainer.appendChild(filterBadge);
//        updateModel(id);
//    } else if (!inputElement.checked && filterBadge) {
//        filterBadge.remove();
//        updateModel(id);
//    }
//}

document.querySelector('.clear-filters').addEventListener('click', () => {
    document.querySelectorAll('.selected-filters').forEach(container => container.innerHTML = '');
    const hiddenFields = document.getElementsByName('filters');
    hiddenFields.forEach(field => field.value = '');
    document.querySelectorAll('.dropdown-content input[type="checkbox"]').forEach(x => x.checked = false);
    updateModel();
});

document.querySelector('.search-btn').addEventListener('click', () => {
    // Process each input text field and add a filter for its value
    document.querySelectorAll('.filter-option input[type="text"]').forEach(input => {
        const value = input.value.trim();
        if (value) { // Ensure the value is not empty before adding a filter
            addFilter(input); // Add the filter for the text input
        }
    });
});


document.querySelector('.filter-container').addEventListener('wheel', (e) => {
    e.stopPropagation();
});

function updateModel(id) {
    const selectedCheckboxes = Array.from(document.querySelectorAll('.filter-category .filter-options .form-check-input:checked'));
    const checkboxValues = selectedCheckboxes.map(checkbox => {
        const label = document.querySelector(`label[for="${checkbox.id}"]`).textContent;
        return label.trim();
    });

    // Collect value from the textbox
    const textboxValue = document.getElementById('txtcompany').value.trim();

    // Combine checkbox values and textbox value into one array
    let values = [...checkboxValues];
    if (textboxValue) {
        values.unshift(textboxValue); // Add the textbox value at the beginning
    }

    // Create a comma-separated string of values
    const modelString = values.join(', ');

    // Check if the element exists and update the hidden input field corresponding to the filter model
    if (id) {
        const hiddenInput = document.getElementById(id + '-filter-model');
        if (hiddenInput) {
            hiddenInput.value = modelString;
        }
    }

    //// Collect values from all selected filter badges
    //const values = Array.from(document.querySelectorAll('.filter-category .filter-badge')).map(badge => {
    //    console.log(badge);
    //    badge.dataset.value
    //});
    //console.log('values ', values);
    //// Create a comma-separated string of values
    //const modelString = values.join(', ');

    //// Check if the element exists and update the hidden input field corresponding to the filter model
    //if (id) {
    //    const hiddenInput = document.getElementById(id + '-filter-model');
    //    if (hiddenInput) {
    //        hiddenInput.value = modelString;
    //    }
    //}
}


function showDropdown4() {
    document.getElementById("dropdownContent4").classList.add("show");
    document.getElementById("dropdownContent4").style.display = "block";
}

function hideDropdown4() {
    document.getElementById("dropdownContent4").classList.remove("show");
    document.getElementById("dropdownContent4").style.display = "none";
}
// Hide dropdown when clicking outside
window.addEventListener('click', function (event) {
    var input = document.getElementById("industrySearch4");
    var dropdown = document.getElementById("dropdownContent4");

    if (!input.contains(event.target) && !dropdown.contains(event.target)) {
        hideDropdown4();
    }
});

// Optional: Close dropdown when pressing "Escape" key
window.addEventListener('keydown', function (event) {
    if (event.key === 'Escape') {
        hideDropdown4();
    }
});
function toggleChildCheckboxes(parentCheckbox) {
    var data = $(parentCheckbox).attr('id');
    var childCheckboxes = $('.' + data);
    childCheckboxes.prop('checked', parentCheckbox.checked);
}

// Function to toggle visibility of subcategories
function toggleSubcategories(subcategoryId, iconId) {
    const subcategoryDiv = document.getElementById(subcategoryId);
    const toggleIcon = document.getElementById(iconId);

    // Toggle subcategories visibility
    if (subcategoryDiv.style.display === "none" || subcategoryDiv.style.display === "") {
        subcategoryDiv.style.display = "block";
        toggleIcon.innerHTML = "&#9660;"; // Down arrow
    } else {
        subcategoryDiv.style.display = "none";
        toggleIcon.innerHTML = "&#8250;"; // Right arrow
    }
}

// Function to sync subcategory checkboxes with the industry checkbox
function syncSubcategories(subcategoryId, industryCheckbox) {
    const subcategoryDiv = document.getElementById(subcategoryId);
    const subcategoryCheckboxes = subcategoryDiv.getElementsByClassName("subcategory-checkbox");

    // Toggle subcategories and their checkboxes based on the industry checkbox state
    if (industryCheckbox.checked) {
        subcategoryDiv.style.display = "block";
        Array.from(subcategoryCheckboxes).forEach(checkbox => checkbox.checked = true);
    } else {
        subcategoryDiv.style.display = "none";
        Array.from(subcategoryCheckboxes).forEach(checkbox => checkbox.checked = false);
    }
}


// Function to toggle a checkbox based on its associated label click
function toggleCheckbox(checkboxId) {
    const checkbox = document.getElementById(checkboxId);
    checkbox.checked = !checkbox.checked;
}

// Function to filter departments
function filterIndustries() {
    const searchInput = document.getElementById("industrySearch").value.toLowerCase();
    const industries = document.getElementsByClassName("industry");

    for (let i = 0; i < industries.length; i++) {
        const industry = industries[i];
        const industryLabel = industry.getElementsByTagName("label")[0].innerHTML.toLowerCase();
        const subcategories = industry.getElementsByClassName("subcategories")[0];
        const subcategoryLabels = subcategories.getElementsByTagName("label");

        let isMatch = false;

        // Check if the industry title matches the search input
        if (industryLabel.indexOf(searchInput) > -1) {
            isMatch = true;
        }

        // Check if any of the subcategory labels match the search input
        for (let j = 0; j < subcategoryLabels.length; j++) {
            const subcategoryLabel = subcategoryLabels[j].innerHTML.toLowerCase();
            if (subcategoryLabel.indexOf(searchInput) > -1) {
                isMatch = true;
                // Automatically show subcategories if a match is found
                subcategories.style.display = "block";
                industry.getElementsByTagName("input")[0].checked = true; // Check the industry checkbox
                document.getElementById(industry.getElementsByTagName("input")[0].id + "ToggleIcon").innerHTML = "&#9660;"; // Change icon to down arrow
                break;
            }
        }

        // Display the industry (and its subcategories) if a match is found
        if (isMatch) {
            industry.style.display = "";
        } else {
            industry.style.display = "none";
            industry.getElementsByTagName("input")[0].checked = false; // Uncheck the industry checkbox
        }
    }
}



// Function to filter industries 
function filterIndustries2() {
    const searchInput = document.getElementById("industrySearch2").value.toLowerCase();
    const industries = document.getElementsByClassName("industry2");

    for (let i = 0; i < industries.length; i++) {
        const industry = industries[i];
        const industryLabel = industry.getElementsByTagName("label")[0].innerHTML.toLowerCase();
        const subcategories = industry.getElementsByClassName("subcategories")[0];
        const subcategoryLabels = subcategories.getElementsByTagName("label");

        let isMatch = false;

        // Check if the industry title matches the search input
        if (industryLabel.indexOf(searchInput) > -1) {
            isMatch = true;
        }

        // Check if any of the subcategory labels match the search input
        for (let j = 0; j < subcategoryLabels.length; j++) {
            const subcategoryLabel = subcategoryLabels[j].innerHTML.toLowerCase();
            if (subcategoryLabel.indexOf(searchInput) > -1) {
                isMatch = true;
                // Automatically show subcategories if a match is found
                subcategories.style.display = "block";
                industry.getElementsByTagName("input")[0].checked = true; // Check the industry checkbox
                document.getElementById(industry.getElementsByTagName("input")[0].id + "ToggleIcon").innerHTML = "&#9660;"; // Change icon to down arrow
                break;
            }
        }

        // Display the industry (and its subcategories) if a match is found
        if (isMatch) {
            industry.style.display = "";
        } else {
            industry.style.display = "none";
            industry.getElementsByTagName("input")[0].checked = false; // Uncheck the industry checkbox
        }
    }
}


// Function to filter Revenue
function filterIndustries3() {
    const searchInput = document.getElementById("industrySearch3").value.toLowerCase();
    const industries = document.getElementsByClassName("industry3");

    for (let i = 0; i < industries.length; i++) {
        const industry = industries[i];
        const industryLabel = industry.getElementsByTagName("label")[0].innerHTML.toLowerCase();
        const subcategories = industry.getElementsByClassName("subcategories")[0];
        const subcategoryLabels = subcategories.getElementsByTagName("label");

        let isMatch = false;

        // Check if the industry title matches the search input
        if (industryLabel.indexOf(searchInput) > -1) {
            isMatch = true;
        }

        // Check if any of the subcategory labels match the search input
        for (let j = 0; j < subcategoryLabels.length; j++) {
            const subcategoryLabel = subcategoryLabels[j].innerHTML.toLowerCase();
            if (subcategoryLabel.indexOf(searchInput) > -1) {
                isMatch = true;
                // Automatically show subcategories if a match is found
                subcategories.style.display = "block";
                industry.getElementsByTagName("input")[0].checked = true; // Check the industry checkbox
                document.getElementById(industry.getElementsByTagName("input")[0].id + "ToggleIcon").innerHTML = "&#9660;"; // Change icon to down arrow
                break;
            }
        }

        // Display the industry (and its subcategories) if a match is found
        if (isMatch) {
            industry.style.display = "";
        } else {
            industry.style.display = "none";
            industry.getElementsByTagName("input")[0].checked = false; // Uncheck the industry checkbox
        }
    }
}


// Function to filter company sizes
function filterIndustries4() {
    debugger;
    const searchInput = document.getElementById("industrySearch4").value.toLowerCase();
    const industries = document.getElementsByClassName("industry4");

    for (let i = 0; i < industries.length; i++) {
        const industry = industries[i];
        const industryLabel = industry.getElementsByTagName("label")[0].innerHTML.toLowerCase();
        const subcategories = industry.getElementsByClassName("subcategories")[0];
        const subcategoryLabels = subcategories.getElementsByTagName("label");

        let isMatch = false;

        // Check if the industry title matches the search input
        if (industryLabel.indexOf(searchInput) > -1) {
            isMatch = true;
        }

        // Check if any of the subcategory labels match the search input
        for (let j = 0; j < subcategoryLabels.length; j++) {
            const subcategoryLabel = subcategoryLabels[j].innerHTML.toLowerCase();
            if (subcategoryLabel.indexOf(searchInput) > -1) {
                isMatch = true;
                // Automatically show subcategories if a match is found
                subcategories.style.display = "block";
                industry.getElementsByTagName("input")[0].checked = true; // Check the industry checkbox
                document.getElementById(industry.getElementsByTagName("input")[0].id + "ToggleIcon").innerHTML = "&#9660;"; // Change icon to down arrow
                break;
            }
        }

        // Display the industry (and its subcategories) if a match is found
        if (isMatch) {
            industry.style.display = "";
        } else {
            industry.style.display = "none";
            industry.getElementsByTagName("input")[0].checked = false; // Uncheck the industry checkbox
        }
    }
}


// Function to filter Level
function filterIndustries5() {
    const searchInput = document.getElementById("industrySearch5").value.toLowerCase();
    const industries = document.getElementsByClassName("industry5");

    for (let i = 0; i < industries.length; i++) {
        const industry = industries[i];
        const industryLabel = industry.getElementsByTagName("label")[0].innerHTML.toLowerCase();
        const subcategories = industry.getElementsByClassName("subcategories")[0];
        const subcategoryLabels = subcategories.getElementsByTagName("label");

        let isMatch = false;

        // Check if the industry title matches the search input
        if (industryLabel.indexOf(searchInput) > -1) {
            isMatch = true;
        }

        // Check if any of the subcategory labels match the search input
        for (let j = 0; j < subcategoryLabels.length; j++) {
            const subcategoryLabel = subcategoryLabels[j].innerHTML.toLowerCase();
            if (subcategoryLabel.indexOf(searchInput) > -1) {
                isMatch = true;
                // Automatically show subcategories if a match is found
                subcategories.style.display = "block";
                industry.getElementsByTagName("input")[0].checked = true; // Check the industry checkbox
                document.getElementById(industry.getElementsByTagName("input")[0].id + "ToggleIcon").innerHTML = "&#9660;"; // Change icon to down arrow
                break;
            }
        }

        // Display the industry (and its subcategories) if a match is found
        if (isMatch) {
            industry.style.display = "";
        } else {
            industry.style.display = "none";
            industry.getElementsByTagName("input")[0].checked = false; // Uncheck the industry checkbox
        }
    }
}

function filterIndustries6() {
    const searchInput = document.getElementById("industrySearch6").value.toLowerCase();
    const industries = document.getElementsByClassName("industry6");

    for (let i = 0; i < industries.length; i++) {
        const industry = industries[i];
        const industryLabel = industry.getElementsByTagName("label")[0].innerHTML.toLowerCase();
        const subcategories = industry.getElementsByClassName("subcategories")[0];
        const subcategoryLabels = subcategories.getElementsByTagName("label");

        let isMatch = false;

        // Check if the industry title matches the search input
        if (industryLabel.indexOf(searchInput) > -1) {
            isMatch = true;
        }

        // Check if any of the subcategory labels match the search input
        for (let j = 0; j < subcategoryLabels.length; j++) {
            const subcategoryLabel = subcategoryLabels[j].innerHTML.toLowerCase();
            if (subcategoryLabel.indexOf(searchInput) > -1) {
                isMatch = true;
                // Automatically show subcategories if a match is found
                subcategories.style.display = "block";
                industry.getElementsByTagName("input")[0].checked = true; // Check the industry checkbox
                document.getElementById(industry.getElementsByTagName("input")[0].id + "ToggleIcon").innerHTML = "&#9660;"; // Change icon to down arrow
                break;
            }
        }

        // Display the industry (and its subcategories) if a match is found
        if (isMatch) {
            industry.style.display = "";
        } else {
            industry.style.display = "none";
            industry.getElementsByTagName("input")[0].checked = false; // Uncheck the industry checkbox
        }
    }
}

// Function to show the dropdown content
function showDropdown() {
    const dropdownContent = document.getElementById("dropdownContent");
    dropdownContent.style.display = "block"; // Show the dropdown content when search bar is clicked
}

function showDropdown2() {
    const dropdownContent2 = document.getElementById("dropdownContent2");
    dropdownContent2.style.display = "block"; // Show the dropdown content when search bar is clicked
}

function showDropdown3() {
    const dropdownContent3 = document.getElementById("dropdownContent3");
    dropdownContent3.style.display = "block"; // Show the dropdown content when search bar is clicked
}

function showDropdown4() {
    const dropdownContent4 = document.getElementById("dropdownContent4");
    dropdownContent4.style.display = "block"; // Show the dropdown content when search bar is clicked
}

function showDropdown5() {
    const dropdownContent5 = document.getElementById("dropdownContent5");
    dropdownContent5.style.display = "block"; // Show the dropdown content when search bar is clicked
    document.getElementById("CSuiteSubcategories").style.display = "block";
}

function showDropdown6() {
    const dropdownContent6 = document.getElementById("dropdownContent6");
    dropdownContent6.style.display = "block"; // Show the dropdown content when search bar is clicked
}


// Function to hide the dropdown content when clicking outside
function hideDropdown(event) {
    const dropdownContent = document.getElementById("dropdownContent");
    const dropdownContent2 = document.getElementById("dropdownContent2");
    const dropdownContent3 = document.getElementById("dropdownContent3");
    const dropdownContent4 = document.getElementById("dropdownContent4");
    const dropdownContent5 = document.getElementById("dropdownContent5");
    const dropdownContent6 = document.getElementById("dropdownContent6");

    const searchBar = document.getElementById("industrySearch");
    const searchBar2 = document.getElementById("industrySearch2");
    const searchBar3 = document.getElementById("industrySearch3");
    const searchBar4 = document.getElementById("industrySearch4");
    const searchBar5 = document.getElementById("industrySearch5");
    const searchBar6 = document.getElementById("industrySearch6");


    // Check if the click target is outside the dropdown and search bar
    if (!dropdownContent.contains(event.target) && !searchBar.contains(event.target)) {
        dropdownContent.style.display = "none";
    }

    if (!dropdownContent2.contains(event.target) && !searchBar2.contains(event.target)) {
        dropdownContent2.style.display = "none";
    }

    if (!dropdownContent3.contains(event.target) && !searchBar3.contains(event.target)) {
        dropdownContent3.style.display = "none";
    }

    if (!dropdownContent4.contains(event.target) && !searchBar4.contains(event.target)) {
        dropdownContent4.style.display = "none";
    }


    if (!dropdownContent5.contains(event.target) && !searchBar5.contains(event.target)) {
        dropdownContent5.style.display = "none";
    }

    //if (!dropdownContent6.contains(event.target) && !searchBar6.contains(event.target)) {
    //    dropdownContent6.style.display = "none";
    //}
}



// Add event listener to the document to detect clicks outside the dropdown
document.addEventListener("click", function (event) {
    hideDropdown(event);

});





