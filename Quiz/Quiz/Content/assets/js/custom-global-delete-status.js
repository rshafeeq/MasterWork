
       $(document).ready(function () {

           $(document).on("click", ".js-delete-button", function (e) {
               var button = $(e.target);
               bootbox.dialog({
                   message: 'Are you sure you want to delete this record?',
                   title: 'Confirm Delete',
                   buttons: {
                       yes: {
                           label: 'Yes',
                           className: "btn-danger",
                           callback: function () {
                               $.post(button.attr("data-url"), { Id: button.attr("data-id") })
                                       .done(function (data) {
                                           bootbox.hideAll();
                                           button.closest('tr').remove();
                                           bootbox.alert({
                                               title: 'Success Delete',
                                               message: 'Record Deleted Successfully',
                                               className: "modal-success",
                                               buttons: {
                                                   ok: {
                                                       label: 'Ok'
                                                   }
                                               }
                                           });
                                       })
                                       .fail(function () {
                                           bootbox.alert({
                                               title: 'Something Failed !!',
                                               message: "Failed to dalete the record",
                                               className: "modal-danger",
                                               buttons: {
                                                   ok: {
                                                       label: 'تم'
                                                   }
                                               }
                                           });
                                       });
                           }
                       },
                       no: {
                           label: 'No',
                           className: "btn-default",
                           callback: function () {
                               bootbox.hideAll();
                           }
                       }
                   },
                   className: "modal-danger"
               });
           });

           $(document).on("click", ".js-status-button", function (e) {
               var button = $(e.target);
               bootbox.dialog({
                   message: 'Are you sure to change the status?',
                   title: 'Change status confirmation',
                   buttons: {
                       yes: {
                           label: 'Yes',
                           className: "btn-danger",
                           callback: function () {
                               $.post(button.attr("data-url"), { Id: button.attr("data-id") })
                                           .done(function (obj) {
                                               bootbox.hideAll();
                                               bootbox.alert({
                                                   title: 'Success status change',
                                                   message: 'Record status changed successfully',
                                                   className: "modal-success",
                                                   buttons: {
                                                       ok: {
                                                           label: 'Ok'
                                                       }
                                                   }
                                               });
                                               location.reload();
                                           })
                                           .fail(function () {
                                               bootbox.alert({
                                                   title: 'Something Failed !!',
                                                   message: "Failed to change the status",
                                                   className: "modal-danger",
                                                   buttons: {
                                                       ok: {
                                                           label: 'Done'
                                                       }
                                                   }
                                               });
                                           });
                           }
                       },
                       no: {
                           label: 'No',
                           className: "btn-default",
                           callback: function () {
                               bootbox.hideAll();
                           }
                       }
                   },
                   className: "modal-danger"
               });
           });

           $(document).on("click", ".js-lockuser-button", function (e) {
               var button = $(e.target);
               bootbox.dialog({
                   message: 'Are you sure to lock/unlock the user?',
                   title: 'Lock Change Confirmation',
                   buttons: {
                       yes: {
                           label: 'Yes',
                           className: "btn-danger",
                           callback: function () {
                               $.post(button.attr("data-url"), { Id: button.attr("data-id") })
                                           .done(function (obj) {
                                               bootbox.hideAll();
                                               bootbox.alert({
                                                   title: 'Success change',
                                                   message: 'Record changed successfully',
                                                   className: "modal-success",
                                                   buttons: {
                                                       ok: {
                                                           label: 'Ok'
                                                       }
                                                   }
                                               });
                                               location.reload();
                                           })
                                           .fail(function () {
                                               bootbox.alert({
                                                   title: 'Something Failed !!',
                                                   message: "Failed to change",
                                                   className: "modal-danger",
                                                   buttons: {
                                                       ok: {
                                                           label: 'Done'
                                                       }
                                                   }
                                               });
                                           });
                           }
                       },
                       no: {
                           label: 'No',
                           className: "btn-default",
                           callback: function () {
                               bootbox.hideAll();
                           }
                       }
                   },
                   className: "modal-danger"
               });
           });

       });
