Suoxd.Init = function () {
	$(".button_ctl").each(function(){
		$(this).click(function(){
			data={
				id : $(this)[0].id
			};
			console.log(data);

			Suoxd.Common.ajaxRequest("/cmd", data, function () {
				console.log("success");
			});
		});
	});
};
