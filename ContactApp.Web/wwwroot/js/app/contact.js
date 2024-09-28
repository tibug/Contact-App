$(document).ready(function () {
    var dataTable = initializeDataTable();

    $('#btnclear').click(function () {
        $('#accordionExample input[type="text"]').val('');
        $('#accordionExample input[type="number"]').val('');
        $('#accordionExample input[type="checkbox"]').prop('checked', false);
    });

    $('#btnexport').click(function () {
        showModal();
    });
    $('.close').click(function () {
        hideModal();
    });
    $('#btnfinalexport').click(function () {
        var jsonData = getJsonData();
        $('#btnfinalexport').html('<i class="bi bi-file-earmark-spreadsheet-fill"></i> Please wait...')

        debugger;
        var limit = parseInt($('#txtrem').val());

        if (limit <= 0) {
            toastr.error('You’ve reached your daily export. Please Upgrade your plan.');
            $('#btnfinalexport').html('<i class="bi bi-file-earmark-spreadsheet-fill"></i> Excel');
            hideModal();
            return false;
        }
        $.ajax({
            type: "POST",
            url: "/ContactFilter/ExportContactsToCsv",
            contentType: 'application/json',
            xhrFields: {
                responseType: 'blob'
            },
            data: JSON.stringify(jsonData),
            success: function (data, textStatus, xhr) {

                var limitavailable = xhr.getResponseHeader('limitavailable');
                var perlimitavailable = xhr.getResponseHeader('perlimitavailable');
                var remdata = xhr.getResponseHeader('remval');
                if (remdata == '') {
                    $('#txtrem').val('0');
                } else {
                    $('#txtrem').val(remdata);
                }

                if (limitavailable == "Y" && perlimitavailable == "Y") {
                    var blob = new Blob([data], { type: 'text/csv' });
                    var filename = 'exported_data.csv';
                    saveAs(blob, filename);
                }
                if (limitavailable == "N") {
                    toastr.error('You’ve reached your daily export. Please Upgrade your plan.');
                    $('#btnfinalexport').html('<i class="bi bi-file-earmark-spreadsheet-fill"></i> Excel');
                    hideModal();
                    return false;
                }

                if (perlimitavailable == "N") {
                    toastr.error('Value entered more than your per eport limit. Please Upgrade your plan.');
                    $('#btnfinalexport').html('<i class="bi bi-file-earmark-spreadsheet-fill"></i> Excel');
                    hideModal();
                    return false;
                }


                hideModal();
                $('#btnfinalexport').html('<i class="bi bi-file-earmark-spreadsheet-fill"></i> Excel')
            },
            error: function (error) {
                $('#btnfinalexport').html('<i class="bi bi-file-earmark-spreadsheet-fill"></i> Excel')
                console.log("Error fetching data: ", error);
            }
        });
    });
    $('.modal2close').click(function () {
        $('#myModal2').modal('hide');
    });
    $('#btnsearch').click(function () {
        var checkuser = $.cookie("userCheck");
        var searchlimit = $.cookie("searchlimit");
        if (checkuser != "" && checkuser == "free") {
            if (searchlimit == "" || searchlimit == null || searchlimit == undefined) {
                $.removeCookie("searchlimit");
                $.cookie("searchlimit", "1", { expires: 1 });
                console.log("searchlimit--------->", searchlimit);
            } else if (searchlimit == "1") {
                $.removeCookie("searchlimit");
                $.cookie("searchlimit", "2", { expires: 1 });
                console.log("searchlimit--------->", searchlimit);
            }
            else if (searchlimit == "2") {
                $.removeCookie("searchlimit");
                $.cookie("searchlimit", "3", { expires: 1 });
                console.log("searchlimit--------->", searchlimit);
            }
            else if (searchlimit == "3") {
                $.removeCookie("searchlimit");
                $.cookie("searchlimit", "4", { expires: 1 });
                console.log("searchlimit--------->", searchlimit);
            }
            else if (searchlimit == "4") {
                $.removeCookie("searchlimit");
                $.cookie("searchlimit", "5", { expires: 1 });
                console.log("searchlimit--------->", searchlimit);

            }
            else if (searchlimit == "5") {
                $.removeCookie("searchlimit");
                $.cookie("searchlimit", "6", { expires: 1 });
                console.log("searchlimit--------->", searchlimit);
            }
            else if (searchlimit == "6") {
                $.removeCookie("searchlimit");
                $.cookie("searchlimit", "7", { expires: 1 });
                console.log("searchlimit--------->", searchlimit);

            }
            else if (searchlimit == "7") {
                $("#myModal2").modal('show');
                toastr.error('You’ve reached your daily export. Please Upgrade your plan.');
                console.log("searchlimit--------->", searchlimit);
                return;
            }
        }
        $('#btnsearch').html('<i class="bi bi-search"></i> Wait...');
        var jsonData = getJsonData();
        console.log('Sending Data:', jsonData); // Debugging
        dataTable.ajax.reload(null, false); // false to keep pagination

        // Update the AJAX `data` function to pass the custom search data
        dataTable.settings()[0].ajax.data = function (data) {
            var requestData = {
                ...data,       // Default DataTables parameters
                ...jsonData    // Your custom filters
            };
            console.log('Request Data:', requestData); // Debugging
            return JSON.stringify(requestData); // Send the merged data
        };

        $('#btnsearch').html('<i class="bi bi-search"></i> Search');
    });
    const sidebarToggle = document.querySelector("#sidebar-toggle");
    const sidebar = document.querySelector("#sidebar");
    sidebarToggle.addEventListener("click", function () {
        //document.querySelector("#sidebar").classList.toggle("collapsed");
        if (sidebar.classList.contains("hidden")) {
            // If hidden, remove the 'hidden' class to show the sidebar
            sidebar.classList.remove("hidden");
            $("#divclear").show();
        } else {
            // If not hidden, add the 'hidden' class to hide the sidebar
            sidebar.classList.add("hidden");
            $("#divclear").hide();
        }
    });
});
function showModal() {
    $('#myModal').modal('show');
}
function hideModal() {
    $('#myModal').modal('hide');
}
function initializeDataTable() {
    var table = $('#UsersTable').DataTable({
        searching: false,
        lengthChange: false,
        processing: true,
        serverSide: true, // Enable server-side processing
        "dom": '<"top"i>rt<"bottom"p><"clear">',
        "pageLength": 25, // Default page size
        "order": [[5, 'asc']], // Default sort order
        "pagingType": "full_numbers",
        "lengthChange": false,
        //"ordering": false,
        /*        "info": true,*/
        "ajax": {
            url: '/ContactFilter/GetAll', // Endpoint for server-side data
            type: 'POST',
            contentType: 'application/json',
            data: function (data) {
                var customData = getJsonData();
                data.pageLimit = 50; // Pass the page limit to the server
                var requestData = {
                    ...data, // Default DataTables parameters
                    ...customData // Your custom filters
                };
                console.log('Request Data:', requestData); // Debugging
                return JSON.stringify(requestData); // Serialize combined data
            },
            dataSrc: function (data) {
                var formattedCount = Number(data.recordsFiltered).toLocaleString();
                if (data.recordsFiltered >= 1000000) {
                    $('#tcount').text('Verified Contacts (1M+)');
                } else {
                    $('#tcount').text('Verified Contacts (' + formattedCount + ')');
                }
                return data.data; // Return the data array for DataTables
            },
            beforeSend: function () {
                //$(".dialog-background").show(); // Show progress bar before sending the request
            },
            complete: function () {
                //$(".dialog-background").hide(); // Hide progress bar after request completes
            },
            error: function (xhr, error, thrown) {
                console.error('Error loading data:', error); // Log errors for debugging
            }
        },
        "columns": [
            {
                'data': 'name',
                "autoWidth": true,
                "render": function (data, type, row) {
                    return '<div style="display: flex; align-items: center;">' +
                        '<a href="' + row.linkedInUrl + '" target="_blank">' +
                        '<img src="/images/linkedin.png" alt="LinkedIn" width="25px" height="25px" style="margin-right: 10px;">' +
                        '</a>' +
                        '<span>' + data + '</span>' +
                        '</div>';
                }
            },
            { 'data': 'leadTitle', "autoWidth": true },
            { 'data': 'seniorityLevel', "autoWidth": true },
            { 'data': 'leadDivision', "autoWidth": true },
            {
                'data': 'companyName',
                "autoWidth": true,
                "render": function (data, type, row) {
                    var imageUrl = row.companyProfileImageUrl;
                    if (imageUrl && !imageUrl.startsWith("http")) {
                        imageUrl = "https://" + imageUrl;
                    }
                    var imageHtml = imageUrl ? '<img src="' + imageUrl + '" alt="Company Image" class="lazyload" style="width:30px; height:30px; margin-right: 10px;" />' : '';

                    // Create a clickable link that directs to the company page
                    var companyLink = '<a href="/company/' + row.companyId + '" style="text-decoration: none; color: inherit;">' +
                        '<div style="display: flex; align-items: center;">' +
                        imageHtml +
                        '<span>' + data + '</span>' +
                        '</div>' +
                        '</a>';

                    return companyLink;
                }
            },
            { 'data': 'leadLocation', "autoWidth": true }
        ],
        "columnDefs": [
            {
                targets: '_all',
                render: function (data) {
                    return `<div class="truncate" title="${data}">${data || ''}</div>`;
                }
            }
        ],
        "drawCallback": function (settings) {
            var api = this.api();
            var info = api.page.info();
            var pages = Math.ceil(info.recordsTotal / info.length);
            var currentPage = info.page + 1;

            // Cache jQuery selectors
            var $nextButton = $('.paginate_button.next');
            var $lastButton = $('.paginate_button.last');

            // Initialize button states
            resetButtonStates($nextButton, $lastButton);

            // Handle button states
            handleButtonStates($nextButton, $lastButton, currentPage, pages);

            // Set up button click events
            setupButtonClickEvents($nextButton, $lastButton, api, currentPage, pages);

            // Disable page buttons beyond 50
            disableExtraPaginationButtons(pages);
        }
    });

    function resetButtonStates($nextButton, $lastButton) {
        $nextButton.removeClass('disabled');
        $lastButton.removeClass('disabled');
    }

    function handleButtonStates($nextButton, $lastButton, currentPage, pages) {
        if (currentPage === 50) {
            $nextButton.addClass('disabled');
        }
        if (currentPage === 50 || pages <= 50) {
            $lastButton.addClass('disabled');
        }
    }

    function setupButtonClickEvents($nextButton, $lastButton, api, currentPage, pages) {
        $lastButton.off('click').on('click', function () {
            if (pages > 50 && currentPage < 50) {
                api.page(49).draw(false); // Go to page 50 (index 49)
            }
        });

        $nextButton.off('click').on('click', function () {
            if (currentPage < 50) {
                api.page(currentPage).draw(false); // Move to the next page
            }
        });
    }

    function disableExtraPaginationButtons(pages) {
        if (pages > 50) {
            var lastPageNumber = pages;
            for (var i = 51; i <= lastPageNumber; i++) {
                $('.paginate_button:contains(' + i + ')').addClass('disabled').off('click'); // Disable buttons greater than 50
            }
        }
    }

    $('#UsersTable tbody').on('click', 'tr', function () {
        var tr = $(this);
        var row = table.row(tr);

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('shown');
        } else {
            row.child(format(row.data())).show();
            tr.addClass('shown');
        }
    });

    // Format function for row details
    function format(data) {
        const description = data.companyDescription || 'Unavailable';
        const descriptionPreview = description !== 'Unavailable' ? description.split(" ").slice(0, 3).join(" ") : 'Unavailable';
        const fullDescription = description !== 'Unavailable' ? description : '';

        // Determine the label (A+, A, B) and circle color based on the email score
        const emailScore = data.emailScore || 'Unavailable';
        let circleContent = '', circleColor = '', scoreTooltip = '';

        if (emailScore >= 99) {
            circleContent = 'A+';
            circleColor = '#003366'; // Darker blue
        } else if (emailScore >= 85) {
            circleContent = 'A';
            circleColor = '#336699'; // Medium blue
        } else if (emailScore >= 75) {
            circleContent = 'B';
            circleColor = '#6699CC'; // Lighter blue
        } else if (emailScore >= 70) {
            circleContent = 'B';
            circleColor = '#99CCFF'; // Lightest blue
        }

        if (emailScore !== 'Unavailable') {
            scoreTooltip = `Contact Accuracy Score: ${emailScore}`;
        }

        return `<div class="detail-view" style="display: grid; gap: 10px; grid-template-columns: repeat(4, 1fr); padding: 10px;">
                                                                                    <div class="tile" style="border: 1px solid #ddd; border-radius: 8px; padding: 10px;">
                                                                                        <b>Direct Phone:</b><br>
                                                                                            <span style="font-family: Arial, sans-serif; font-size: 16px; color: black;">${data.workPhone || 'Unavailable'}</span>
                                                                                    </div>
                                                                                    <div class="tile" style="border: 1px solid #ddd; border-radius: 8px; padding: 10px;">
                                                                                        <b>Work Email:</b><br>
                                                                                        <span style="font-family: Arial, sans-serif; font-size: 16px; color: black;">
                                                                                            ${data.email || 'Unavailable'}
                                                                                        </span>
                                                                                        ${emailScore !== 'Unavailable' ?
                `<span style="display: inline-block; vertical-align: middle; margin-left: 8px;">
                                                                                                <span style="background-color: ${circleColor}; border-radius: 50%; width: 24px; height: 24px; display: inline-flex; justify-content: center; align-items: center; color: white; font-size: 12px; font-weight: bold;" title="${scoreTooltip}">
                                                                                                    ${circleContent}
                                                                                                </span>
                                                                                            </span>` : ''}
                                                                                    </div>
                                                                                    <div class="tile" style="border: 1px solid #ddd; border-radius: 8px; padding: 10px;">
                                                                                        <b>Company Industry:</b><br>
                                                                                            <span style="font-family: Arial, sans-serif; font-size: 16px; color: black;">${data.companyIndustry || 'Unavailable'}</span>
                                                                                    </div>
                                                                                    <div class="tile" style="border: 1px solid #ddd; border-radius: 8px; padding: 10px;">
                                                                                        <b>Company Revenue (M):</b><br>
                                                                                            <span style="font-family: Arial, sans-serif; font-size: 16px; color: black;">${data.revenueRange || 'Unavailable'}</span>
                                                                                    </div>
                                                                                    <div class="tile" style="border: 1px solid #ddd; border-radius: 8px; padding: 10px;">
                                                                                        <b>Mobile:</b><br>
                                                                                        <span style="font-family: Arial, sans-serif; font-size: 16px; color: black;">${data.phone || 'Unavailable'}</span>
                                                                                    </div>
                                                                                    <div class="tile" style="border: 1px solid #ddd; border-radius: 8px; padding: 10px;">
                                                                                        <b>Company Size:</b><br>
                                                                                            <span style="font-family: Arial, sans-serif; font-size: 16px; color: black;">${data.companySize || 'Unavailable'}</span>
                                                                                    </div>
                                                                                    <div class="tile" style="border: 1px solid #ddd; border-radius: 8px; padding: 10px;">
                                                                                        <b>HQ Phone:</b><br>
                                                                                            <span style="font-family: Arial, sans-serif; font-size: 16px; color: black;">${data.companyPhoneNumbers || 'Unavailable'}</span>
                                                                                    </div>
                                                                                    <div class="tile" style="border: 1px solid #ddd; border-radius: 8px; padding: 10px;">
                                                                                        <b>Company Profiles:</b><br>
                                                                                                ${data.companyWebsite ? `<a href="${data.companyWebsite}" target="_blank"><i class="fas fa-globe" style="font-size: 18px;"></i></a>` : '<span>Unavailable</span>'}
                                                                                                ${data.linkedInUrl ? `&nbsp;&nbsp;&nbsp;<a href="${data.linkedInUrl}" target="_blank"><i class="fab fa-linkedin" style="font-size: 18px;"></i></a>` : ''}
                                                                                                ${data.companyFacebookPage ? `&nbsp;&nbsp;&nbsp;<a href="${data.companyFacebookPage}" target="_blank"><i class="fab fa-facebook" style="font-size: 18px;"></i></a>` : ''}
                                                                                                ${data.companyTwitterPage ? `&nbsp;&nbsp;&nbsp;<a href="${data.companyTwitterPage}" target="_blank"><i class="fab fa-x-twitter" style="font-size: 18px;"></i></a>` : ''}
                                                                                    </div>
                                                                                    <div class="tile" style="border: 1px solid #ddd; border-radius: 8px; padding: 10px;">
                                                                                        <b>Description:</b><br>
                                                                                        <span class="description-preview" style="color: black;" title="${fullDescription}">${descriptionPreview}</span>
                                                                                    </div>
                                                                                </div>`;
    }

    return table;
}

function getJsonData() {
    var txtCompanyNaicsCode = $('#txtnaicscode-filter-model').val();
    var companyNaicsCodeValue = txtCompanyNaicsCode ? parseInt(txtCompanyNaicsCode, 10) : null;

    var txtCompanySicCode = $('#txtsiccode-filter-model').val();
    var companySicCodeValue = txtCompanySicCode ? parseInt(txtCompanySicCode, 10) : null;

    var data = {
        company_name: $('#txtcompany-filter-model').val(),
        keyword: $('#txtkeyword-filter-model').val(),
        company_industry: getCheckedValues('heading'),
        company_size: getCheckedValues('selectAllcompanysize'),
        revenue_range: getCheckedValues('selectAllrevenue'),
        seniority_level: getCheckedValues('selectAlllevel'),
        lead_division: getCheckedValues('departjob'),
        email_score: getCheckedValues('selectAllemalscore'),
        lead_titles: $('#txtjobtitle-filter-model').val(),
        lead_location: $('#txtleadlocation-filter-model').val(),
        name: $('#txtname-filter-model').val(),
        email: $('#txtworkemail-filter-model').val(),
        company_naics_code: companyNaicsCodeValue,
        company_sic_code: companySicCodeValue,
        PerExportLimit: $('#txtPerExportLimit').val(),
        company_sector: $('#txtSector-filter-model').val()
    };
    console.log('Generated JSON Data:', data); // Debugging
    return data;
}

function getCheckedValues(className) {
    var selectedValues = [];
    $('.' + className + ':checked').each(function () {
        var value = $(this).val();
        console.log('Checkbox Value:', value); // Log to verify
        if (value) {
            selectedValues.push(value); // Add value if it's not null or empty
        }
    });
    return selectedValues;
}


