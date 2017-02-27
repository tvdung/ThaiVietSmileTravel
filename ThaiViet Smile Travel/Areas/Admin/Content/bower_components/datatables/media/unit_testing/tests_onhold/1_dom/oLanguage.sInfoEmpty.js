// DATA_TEMPLATE: dom_data
oTest.fnStart( "oLanguage.sInfoEmpty" );

$(document).ready( function () {
	/* Check the default */
	var oTable = $('#example').dataTable();
	var oSettings = oTable.fnSettings();
	
	oTest.fnTest( 
		"Info empty language is 'Hi?n th? 0 d?n 0 c?a 0 tour' by default",
		function () { oTable.fnFilter("nothinghere"); },
		function () { return oSettings.oLanguage.sInfoEmpty == "Hi?n th? 0 d?n 0 c?a 0 tour"; }
	);
	
	oTest.fnTest( 
		"Info empty language default is in the DOM",
		null,
		function () {
			var bReturn = document.getElementById('example_info').innerHTML.replace( 
				' '+oSettings.oLanguage.sInfoFiltered.replace( '_MAX_', '57' ), "" ) ==
					"Hi?n th? 0 d?n 0 c?a 0 tour";
			return bReturn;
		}
	);
	
	
	oTest.fnTest( 
		"Info empty language can be defined",
		function () {
			oSession.fnRestore();
			oTable = $('#example').dataTable( {
				"oLanguage": {
					"sInfoEmpty": "unit test"
				}
			} );
			oSettings = oTable.fnSettings();
			oTable.fnFilter("nothinghere");
		},
		function () { return oSettings.oLanguage.sInfoEmpty == "unit test"; }
	);
	
	oTest.fnTest( 
		"Info empty language default is in the DOM",
		null,
		function () {
			var bReturn = document.getElementById('example_info').innerHTML.replace( 
				' '+oSettings.oLanguage.sInfoFiltered.replace( '_MAX_', '57' ), "" ) ==
					"unit test";
			return bReturn;
		}
	);
	
	
	oTest.fnTest( 
		"Macro's replaced",
		function () {
			oSession.fnRestore();
			oTable = $('#example').dataTable( {
				"oLanguage": {
					"sInfoEmpty": "unit _START_ _END_ _TOTAL_ test"
				}
			} );
			oTable.fnFilter("nothinghere");
		},
		function () {
			var bReturn = document.getElementById('example_info').innerHTML.replace( 
				' '+oSettings.oLanguage.sInfoFiltered.replace( '_MAX_', '57' ), "" ) ==
					"unit 1 0 0 test";
			return bReturn;
		}
	);
	
	
	oTest.fnComplete();
} );