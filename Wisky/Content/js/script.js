var WIFI = window.WIFI || {};

WIFI.video = function () {
	$(document).ready(function () {
		
		$('[data-type="hidden"]').hide();
		initClock();
		
		$('[ data-type="redirect"]').click(function(e) {
			var me = $(this);
			$('body').fadeOut(1000, function() {
				window.location = me.attr('data-href');
			});
		});
	});
	
	function initClock () {
		window.clock = new QuizClock('#inputClock', window["duration"], function () {
            $('[data-type="hidden"]').fadeIn();
			$('#msgClock').fadeOut(function() {
				$('#msgSuccess').fadeIn();
			});
        });
		window.clock.start();
	}
}

WIFI.video();