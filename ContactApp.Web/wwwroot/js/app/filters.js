function showDropdown(input) {
    const dropdownMenu = input.nextElementSibling;
    dropdownMenu.classList.add('show');

    const subcategories = document.querySelectorAll('.subcategories');
    if (subcategories.length > 0) {
        subcategories.forEach(subcat => {
            subcat.style.display = 'none';
            const toggleIcon = subcat.previousElementSibling.querySelector('.toggle-icon');
            if (toggleIcon) {
                toggleIcon.innerHTML = '&#8250;';
            }
        });
    }
    document.addEventListener('click', function (event) {
        if (!input.contains(event.target) && !dropdownMenu.contains(event.target)) {
            dropdownMenu.classList.remove('show');
        }
    }, { once: true });

    document.addEventListener('keydown', function (event) {
        if (event.key === 'Escape') {
            dropdownMenu.classList.remove('show');
        }
    }, { once: true });
}

function hideDropdownOnBlur(inputElement) {
    inputElement.addEventListener('blur', function () {
        const dropdown = inputElement.nextElementSibling;
        if (dropdown && dropdown.classList.contains('show')) {
            dropdown.classList.remove('show');
        }
    });
}

function filterOptions(inputElement) {
    const searchTerm = inputElement.value.toLowerCase();
    const dropdownContent = inputElement.closest('.filter-option').querySelector('.options-list');
    const options = dropdownContent.getElementsByTagName('li');

    for (let i = 0; i < options.length; i++) {
        const label = options[i].textContent.toLowerCase();
        options[i].style.display = label.includes(searchTerm) ? "" : "none";

        // Hide or show subcategory items based on parent checkbox visibility
        const subcategoryList = options[i].querySelector('.subcategory-list');
        if (subcategoryList) {
            const subcategories = subcategoryList.getElementsByTagName('li');
            let subcategoryVisible = false;
            for (let j = 0; j < subcategories.length; j++) {
                const subcategoryLabel = subcategories[j].textContent.toLowerCase();
                if (subcategoryLabel.includes(searchTerm)) {
                    subcategoryVisible = true;
                    break;
                }
            }
            subcategoryList.style.display = subcategoryVisible ? "block" : "none";
        }
    }
}
function toggleSubcategories(element) {
    const parentCheckbox = element.closest('.checkbox-item').querySelector('.parent-checkbox');
    const subcategories = element.closest('.checkbox-item').querySelector('.subcategories');

    // Toggle visibility of subcategories
    if (subcategories) {
        subcategories.style.display = parentCheckbox.checked ? 'block' : 'none';
    }

    // Check/uncheck child checkboxes based on parent checkbox
    const childCheckboxes = subcategories.querySelectorAll('.child-checkbox');
    childCheckboxes.forEach(childCheckbox => {
        childCheckbox.checked = parentCheckbox.checked;
    });

    // Update toggle icon
    const icon = element.querySelector('.toggle-icon');
    icon.innerHTML = parentCheckbox.checked ? '&#8254;' : '&#8250;';
}

function toggleAllCheckboxes(selectAllCheckbox) {
    const parentDiv = selectAllCheckbox.closest('.options-list');
    var allCheckbox = parentDiv.querySelector('.select-all');
    const checkboxItems = parentDiv.querySelectorAll('.checkbox-item');
    const shouldCheck = !allCheckbox.checked;
    allCheckbox.checked = shouldCheck;

    checkboxItems.forEach(checkboxItem => {
        const checkbox = checkboxItem.querySelector('.parent-checkbox');
        if (checkbox && checkbox.checked !== shouldCheck) {
            checkbox.checked = shouldCheck;
            updateSelectedFilters(checkboxItem, false);
        }
        const subcategories = checkboxItem.closest('.checkbox-item').querySelector('.subcategories');
        if (subcategories) {
            const childCheckboxes = subcategories.querySelectorAll('.child-checkbox');
            childCheckboxes.forEach(childCheckbox => {
                childCheckbox.checked = shouldCheck;
            });
        }

    });
}
function updateSelectedFilters(checkboxItem, toggleState = true) {
    const checkbox = checkboxItem.querySelector('.child-checkbox, .parent-checkbox');
    if (toggleState) {
        checkbox.checked = !checkbox.checked;
    }
    const filterCategoryDiv = checkboxItem.closest('.filter-category');
    const selectedFiltersDiv = filterCategoryDiv.querySelector('.selected-filters');
    const filterType = selectedFiltersDiv.getAttribute('data-filter');
    const labelElement = checkboxItem.querySelector('.form-check-label');
    const badgeValue = labelElement ? labelElement.textContent.trim() : '';

    if (checkbox.checked) {
        if (![...selectedFiltersDiv.querySelectorAll('span')].some(badge => badge.textContent.trim() === badgeValue)) {
            const badge = document.createElement('span');
            badge.className = 'badge bg-primary me-1';
            badge.textContent = badgeValue;

            const closeButton = document.createElement('button');
            closeButton.className = 'btn-close btn-close-white btn-sm ms-1';
            closeButton.innerHTML = '<i class="fas fa-times"></i>';
            closeButton.onclick = function () {
                removeTag(badge);
            };

            badge.appendChild(closeButton);
            selectedFiltersDiv.appendChild(badge);
        }
    } else {
        const badges = selectedFiltersDiv.querySelectorAll('span');
        badges.forEach(badge => {
            if (badge.textContent.trim() === badgeValue) {
                selectedFiltersDiv.removeChild(badge);
            }
        });
    }
    updateSelectAllState(checkboxItem);
}
function updateSelectAllState(checkboxItem) {
    const parentDiv = checkboxItem.closest('.options-list');
    const allCheckbox = parentDiv.querySelector('.select-all');
    const checkboxes = parentDiv.querySelectorAll('.parent-checkbox');
    const allChecked = [...checkboxes].every(checkbox => checkbox.checked);

    allCheckbox.checked = allChecked;
}
function removeTag(tagElement) {
    const tagText = tagElement.textContent.replace("×", "").trim();
    const parentDiv = tagElement.closest('.filter-category');
    const checkboxes = parentDiv.querySelectorAll('.parent-checkbox, .child-checkbox');

    checkboxes.forEach(checkbox => {
        if (checkbox.nextElementSibling.textContent === tagText) {
            updateSelectedFilters(checkbox.closest('.checkbox-item'));
        }
    });
    tagElement.remove();
}
document.querySelector('.clear-filters').addEventListener('click', () => {
    document.querySelectorAll('.selected-filters').forEach(container => container.innerHTML = '');
    document.querySelectorAll('input[type="text"]').forEach(textbox => {
        textbox.value = '';
    });
    document.querySelectorAll('.dropdown-content input[type="checkbox"]').forEach(checkbox => {
        checkbox.checked = false;
        const checkboxValue = checkbox.value;
        const associatedBadge = document.querySelector(`.filter-badge[data-value="${checkboxValue}"]`);
        if (associatedBadge) {
            associatedBadge.remove();
        }
    });
});