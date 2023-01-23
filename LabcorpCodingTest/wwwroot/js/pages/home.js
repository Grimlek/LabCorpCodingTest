function onUpdateButtonClick(event) {
    let $currentTarget = $(event.currentTarget);

    let modalSelector = $currentTarget.attr("data-bs-target"); 
    let $modal = $(modalSelector);
    $modal.modal('show');
    
    $modal.find("input").val("");
    $modal.find(".field-validation-error").text("");
    
    let $hdnIdInput = $modal.find("[id$='EmployeeId']");
    let $tr = $currentTarget.closest("tr");
    let id = $tr.attr("data-id");
    $hdnIdInput.val(id);
}

$(function() {
    $(".btn-update-work-days").on("click", onUpdateButtonClick);
    $(".btn-update-vacation-days").on("click", onUpdateButtonClick);
    
    if (showVacationModalOnPageLoad) {
        $("#vacationDaysModal").modal("show");
    }
    
    if (showWorksModalOnPageLoad) {
        $("#workDaysModal").modal("show");
    }
});
