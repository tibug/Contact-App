var companyName = document.getElementById("txtcompany");
if (companyName) {
    addFilter(companyName);
}

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
    const filterType = getFilterType(inputElement);
    const selectedFiltersContainer = getSelectedFiltersContainer(inputElement);
    let badgeValue = getBadgeValue(inputElement);

    // If the input is empty and not a checkbox, return early
    if (!badgeValue) return false;

    let filterBadge = findExistingBadge(selectedFiltersContainer, badgeValue);

    if (inputElement.type === 'checkbox') {
        handleCheckboxFilter(inputElement, badgeValue, filterType, filterBadge, selectedFiltersContainer);
    }
    //else {
    //    handleTextInputFilter(inputElement, badgeValue, filterType, filterBadge, selectedFiltersContainer);
    //}
}

function handleCheckboxFilter(inputElement, badgeValue, filterType, filterBadge, selectedFiltersContainer) {
    if (inputElement.checked && !filterBadge) {
        addBadge(badgeValue, filterType, inputElement, selectedFiltersContainer);
    } else if (!inputElement.checked && filterBadge) {
        // Get previous checked checkbox values
        const previousCheckedValues = getCheckedValues('filter-options .form-check-input');

        // Remove the badge
        filterBadge.remove();

        // Uncheck the checkbox
        inputElement.checked = false;

        // Update the hidden input field for the filter model
        updateModel(inputElement.id);

        // Log previous checked values for verification
        console.log('Previous Checked Values:', previousCheckedValues);
    }
}

function handleTextInputFilter(inputElement, badgeValue, filterType, filterBadge, selectedFiltersContainer) {
    if (!filterBadge && badgeValue) {
        addBadge(badgeValue, filterType, inputElement, selectedFiltersContainer);
        inputElement.value = ''; // Clear input after adding the filter
    }
}

function addBadge(badgeValue, filterType, inputElement, selectedFiltersContainer) {
    const filterBadge = document.createElement('div');
    filterBadge.className = 'filter-badge';
    filterBadge.dataset.value = badgeValue;
    filterBadge.textContent = `${filterType}: ${badgeValue}`;

    const removeIcon = createRemoveIcon(filterBadge, inputElement);
    filterBadge.appendChild(removeIcon);
    selectedFiltersContainer.appendChild(filterBadge);

    // Update the hidden input field for the filter model
    updateModel(inputElement.id);
}

function createRemoveIcon(filterBadge, inputElement) {
    const removeIcon = document.createElement('i');
    removeIcon.className = 'fas fa-times';
    removeIcon.addEventListener('click', () => {
        filterBadge.remove();
        if (inputElement.type === 'checkbox') {
            inputElement.checked = false; // Ensure the checkbox is unchecked
        }
        updateModel(inputElement.id);
    });
    return removeIcon;
}

function getBadgeValue(inputElement) {
    if (inputElement.type === 'checkbox') {
        const label = document.querySelector(`label[for="${inputElement.id}"]`);
        return label ? label.textContent.trim() : '';
    } else {
        return inputElement.value.trim();
    }
}

function findExistingBadge(selectedFiltersContainer, badgeValue) {
    return selectedFiltersContainer.querySelector(`.filter-badge[data-value="${badgeValue}"]`);
}

function getFilterType(inputElement) {
    return inputElement.closest('.filter-category').querySelector('.filter-header span').textContent.trim();
}

function getSelectedFiltersContainer(inputElement) {
    return inputElement.closest('.filter-category').querySelector('.selected-filters');
}

function getCheckedValues(className) {
    const selectedValues = [];
    const checkboxes = document.querySelectorAll('.' + className + ':checked');
    checkboxes.forEach(checkbox => {
        const value = checkbox.value;
        console.log('Checkbox Value:', value); // Log to verify
        if (value) {
            selectedValues.push(value); // Add value if it's not null or empty
        }
    });
    return selectedValues;
}

function updateModel(id) {
    const selectedCheckboxes = Array.from(document.querySelectorAll('.filter-category .filter-options .form-check-input:checked'));
    const checkboxValues = selectedCheckboxes.map(checkbox => {
        const label = document.querySelector(`label[for="${checkbox.id}"]`).textContent;
        return label.trim();
    });

    const values = [...checkboxValues];

    // Create a comma-separated string of values
    const modelString = values.join(', ');

    if (id) {
        const hiddenInput = document.getElementById(id + '-filter-model');
        if (hiddenInput) {
            hiddenInput.value = modelString; // Update the relevant textbox value
        }
    }
}


//function addFilter(inputElement) {
//    const filterType = getFilterType(inputElement);
//    const selectedFiltersContainer = getSelectedFiltersContainer(inputElement);
//    let badgeValue = getBadgeValue(inputElement);

//    // If the input is empty and not a checkbox, return early
//    if (!badgeValue) return false;

//    let filterBadge = findExistingBadge(selectedFiltersContainer, badgeValue);

//    if (inputElement.type === 'checkbox') {
//        handleCheckboxFilter(inputElement, badgeValue, filterType, filterBadge, selectedFiltersContainer);
//    } else {
//        handleTextInputFilter(inputElement, badgeValue, filterType, filterBadge, selectedFiltersContainer);
//    }
//}

//function handleCheckboxFilter(inputElement, badgeValue, filterType, filterBadge, selectedFiltersContainer) {
//    if (inputElement.checked && !filterBadge) {
//        addBadge(badgeValue, filterType, inputElement, selectedFiltersContainer);
//    } else if (!inputElement.checked && filterBadge) {
//        // Get previous checked checkbox values
//        const previousCheckedValues = getCheckedValues('filter-options .form-check-input');

//        // Remove the badge and clear associated text
//        filterBadge.remove();
//        clearCheckedText(inputElement); // Clear the word "checked"
//        inputElement.checked = false; // Ensure the checkbox is unchecked

//        // Log previous checked values for verification
//        console.log('Previous Checked Values:', previousCheckedValues);
//        updateModel(inputElement.id);
//    }
//}

//function clearCheckedText(inputElement) {
//    const label = document.querySelector(`label[for="${inputElement.id}"]`);
//    if (label) {
//        label.textContent = label.textContent.replace('checked', '').trim();
//    }
//}

//function handleTextInputFilter(inputElement, badgeValue, filterType, filterBadge, selectedFiltersContainer) {
//    if (!filterBadge && badgeValue) {
//        addBadge(badgeValue, filterType, inputElement, selectedFiltersContainer);
//        inputElement.value = ''; // Clear input after adding the filter
//    }
//}

//function addBadge(badgeValue, filterType, inputElement, selectedFiltersContainer) {
//    const filterBadge = document.createElement('div');
//    filterBadge.className = 'filter-badge';
//    filterBadge.dataset.value = badgeValue;
//    filterBadge.textContent = `${filterType}: ${badgeValue}`;

//    const removeIcon = createRemoveIcon(filterBadge, inputElement);
//    filterBadge.appendChild(removeIcon);
//    selectedFiltersContainer.appendChild(filterBadge);

//    updateModel(inputElement.id);
//}

//function createRemoveIcon(filterBadge, inputElement) {
//    const removeIcon = document.createElement('i');
//    removeIcon.className = 'fas fa-times';
//    removeIcon.addEventListener('click', () => {
//        filterBadge.remove();
//        if (inputElement.type === 'checkbox') {
//            inputElement.checked = false;
//            clearCheckedText(inputElement); // Clear the word "checked"
//        }
//        updateModel(inputElement.id);
//    });
//    return removeIcon;
//}

//function getBadgeValue(inputElement) {
//    if (inputElement.type === 'checkbox') {
//        const label = document.querySelector(`label[for="${inputElement.id}"]`);
//        return label ? label.textContent.trim() : '';
//    } else {
//        return inputElement.value.trim();
//    }
//}

//function findExistingBadge(selectedFiltersContainer, badgeValue) {
//    return selectedFiltersContainer.querySelector(`.filter-badge[data-value="${badgeValue}"]`);
//}

//function getFilterType(inputElement) {
//    return inputElement.closest('.filter-category').querySelector('.filter-header span').textContent.trim();
//}

//function getSelectedFiltersContainer(inputElement) {
//    return inputElement.closest('.filter-category').querySelector('.selected-filters');
//}

//function updateModel(id) {
//    const selectedCheckboxes = Array.from(document.querySelectorAll('.filter-category .filter-options .form-check-input:checked'));
//    const checkboxValues = selectedCheckboxes.map(checkbox => {
//        const label = document.querySelector(`label[for="${checkbox.id}"]`).textContent;
//        return label.trim();
//    });

//    const values = [...checkboxValues];

//    // Create a comma-separated string of values
//    const modelString = values.join(', ');

//    if (id) {
//        const hiddenInput = document.getElementById(id + '-filter-model');
//        if (hiddenInput) {
//            hiddenInput.value = modelString;
//        }
//    }
//}

document.querySelector('.clear-filters').addEventListener('click', () => {
    document.querySelectorAll('.selected-filters').forEach(container => container.innerHTML = '');
    const hiddenFields = document.getElementsByName('filters');
    hiddenFields.forEach(field => field.value = '');
    document.querySelectorAll('.dropdown-content input[type="checkbox"]').forEach(checkbox => {
        checkbox.checked = false;
        const checkboxValue = checkbox.value;
        const associatedBadge = document.querySelector(`.filter-badge[data-value="${checkboxValue}"]`);
        if (associatedBadge) {
            associatedBadge.remove();
        }
    });
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
function syncSubcategories(subcategoryId, industryCheckbox) {
    const subcategoryDiv = document.getElementById(subcategoryId);
    const subcategoryCheckboxes = subcategoryDiv.getElementsByClassName("subcategory-checkbox");
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
function showDropdown() {
    const dropdownContent = document.getElementById("dropdownContent");
    dropdownContent.style.display = "block";
}
function hideDropdown(event) {
    const dropdownContent = document.getElementById("dropdownContent");   
    const searchBar = document.getElementById("industrySearch");
    if (!dropdownContent.contains(event.target) && !searchBar.contains(event.target)) {
        dropdownContent.style.display = "none";
    }
}
document.addEventListener("click", function (event) {
    hideDropdown(event);

});





