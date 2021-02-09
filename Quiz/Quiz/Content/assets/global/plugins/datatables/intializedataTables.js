$.extend(true, $.fn.dataTable.defaults, {
    'dom': 'Blfrtip',
    'buttons': [
        {
            'extend': 'copyHtml5',
            'text': '<i class="fa fa-copy"></i> Copy',
            'exportOptions': {
                'columns': ':visible'
            }
        },

            {
                'extend': 'print',
                'text': '<i class="fa fa-print"></i> Print All',
                'customize': function (win) {
                    $(win.document.body).css('font-size', '15px').css('width','100%');
                    var header = $("#PrintHeader").html();
                    var footer = $("#PrintFooter").html();
                    $(win.document.body).prepend(header);
                    $(win.document.body).append(footer);
                    if (header != null)
                        $(win.document.body).find('h1').html('');
                    $(win.document.body).find('.hidden-print').remove();
                    $(win.document.body).find('h1').css('font-size', '15px');
                    $(win.document.body).find('table').addClass('compact').css('width', '100%').css('font-size', 'inherit');
                    $(win.document.body).find('table thead tr').css('font-size', '30px');
                },
                'exportOptions': {
                    'columns': ':visible'
                }

            }
        ,
        {
            'extend': 'print',
            'text': '<i class="fa fa-print"></i> Print Selected',
            'exportOptions': {
                'columns': ':visible',
                'customize': function (win) {

                    $(win.document.body).css('font-size', '10px');
                    $(win.document.body).find('h1').css('font-size', '15px').addClass("bold");
                    $(win.document.body).find('table').addClass('compact').css('width', '100%').css('font-size', 'inherit');

                },
                'modifier': {
                    'selected': true
                }
            }
        },
        {
            'extend': 'excelHtml5',
            'text': '<i class="fa fa-file-o"></i> Excel',
            'exportOptions': {
                'columns': ':visible'
            }
        },
        {
            'extend': 'pdfHtml5',
            'text': '<i class="fa fa-file"></i> Pdf',
            'exportOptions': {
                'columns': ':visible'
            }
        },
        {
            'extend': 'colvis',
            'text': '<i class="fa fa-list"></i> Columns'
        }
    ],
    'select': true,
    "aaSorting": [],
    'columnDefs': [{
        'targets': 'nosort',
        'orderable': false,
        'searchable': false
    }]
});