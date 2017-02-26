var Suoxd;
if (!!Suoxd && (typeof Suoxd != 'object' || Suoxd.name)) throw new Error("namespace already exists!");
Suoxd = {};

Suoxd.name = 'Suo Xudong\'s car control project!';
Suoxd.author = "Suo Xudong";
Suoxd.version = "0.0.1";
(function($) {

    Suoxd.Common = {
		ajaxRequest : _ajaxRequest
	};

	function _ajaxRequest(actionUrl, postData, successHandler,
		errorHandler) {
			setTimeout(function() {
				$.ajax({
					cache : false,
				async : true,
				type : "post",
				url : actionUrl,
				data : postData,
				dataType : "json",
				success : successHandler,
				error : errorHandler
				});
			}, 0);
		}

})(jQuery);
