/*
 Autor : CMOP
 fecha: 22/04/2013
 */
;
(function ($) {
    $.jgrid.extend({
        toExcelFile: function (o) {
            var archivoExporta, hojaExcel;
            archivoExporta = {
                worksheets: [
                    []
                ],
                creator: "Arcmop",
                created: new Date(),
                lastModifiedBy: "Arcmop",
                modified: new Date(),
                activeWorksheet: 0
            };
            hojaExcel = archivoExporta.worksheets[0];
            hojaExcel.name = o.nombre;
            var upperCols = new Array();

            var arrayCabeceras = new Array();
            arrayCabeceras = $(this).getDataIDs();
            var dataFilaGrid = $(this).getRowData(arrayCabeceras[0]);
            var nombreColumnas = new Array();
            var ii = 0;

            var gridColModel = $(this).getGridParam('colModel');

            var gridColNames = $(this).getGridParam('colNames');

            for (var x = 0; x < gridColModel.length; x++) {

                if (gridColModel[x].hidden != true&&gridColModel[x].name != 'Action'&&gridColModel[x].name !='action') {
                    nombreColumnas.push(gridColModel[x].name);

                    var colNme = gridColNames[x].toUpperCase();

                    colNme= colNme.replace('&AACUTE;','Á');
                    colNme= colNme.replace('&EACUTE;','É');
                    colNme= colNme.replace('&IACUTE;','Í');
                    colNme= colNme.replace('&OACUTE;','Ó');
                    colNme= colNme.replace('&UACUTE;','Ú');
                    colNme= colNme.replace("<BR/>",'');

                    upperCols.push(colNme);
                }
            }

            hojaExcel.push(upperCols);
            var dataFilaArchivo;
            for (i = 0; i < arrayCabeceras.length; i++) {
                dataFilaGrid = $(this).getRowData(arrayCabeceras[i]);
                dataFilaArchivo = new Array();
                for (j = 0; j < nombreColumnas.length; j++) {
                    dataFilaArchivo.push(dataFilaGrid[nombreColumnas[j]]);
                }
                hojaExcel.push(dataFilaArchivo);
            }
            return window.location = xlsx(archivoExporta).href();
        },
        exportarTextoCliente: function (o) {
            var arrayCabeceras = new Array();
            arrayCabeceras = $(this).getDataIDs();
            var dataFilaGrid = $(this).getRowData(arrayCabeceras[0]);
            var nombreColumnas = new Array();
            var ii = 0;
            var textoRpta = "";
            for (var i in dataFilaGrid) {
                nombreColumnas[ii++] = i;
                textoRpta = textoRpta + i + "\t";
            }
            textoRpta = textoRpta + "\n";
            for (i = 0; i < arrayCabeceras.length; i++) {
                dataFilaGrid = $(this).getRowData(arrayCabeceras[i]);
                for (j = 0; j < nombreColumnas.length; j++) {
                    textoRpta = textoRpta + dataFilaGrid[nombreColumnas[j]] + "\t";
                }
                textoRpta = textoRpta + "\n";
            }
            textoRpta = textoRpta + "\n";
            return textoRpta;
        }
    });
})(jQuery);