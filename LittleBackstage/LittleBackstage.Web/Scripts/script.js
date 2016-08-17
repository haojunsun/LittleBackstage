// JavaScript Document
jQuery(document).ready(function($){
								
;(function(element) {
		var $respl = $(element);
		var $container = $('.respl-items', $respl);

		$container.imagesLoaded(function() {
			$container.isotope({
				containerStyle: {
					position: 'relative',
					height: 'auto',
					overflow: 'visible'
				},
				itemSelector: '.respl-item',
				sortAscending: true

			});
			_opTionSets();	
			function _opTionSets(){
				var $optionSets = $('.respl-header .respl-option', $respl),
				$optionLinks = $optionSets.find('a');
				$optionLinks.each(function(){
					$(this).click(function(){
						var $this = $(this);
						
						var $optionSet = $this.parents('.respl-option');

						$this.parent().addClass('select').siblings().removeClass('select');
						
						var options = {},key = $optionSet.attr('data-option-key'),value = $this.attr('data-rl_value');
						
						value = value === 'false' ? false: value;
						
						options[key] = value;
						
						$container.isotope(options);
						
						return false;
					});
				});
			}

		});
	})('#yhc_responsive');
	
	//$(".ProductList li").hover(function(){
										
	//	$(this).find(".hoverline").fadeIn().end().find(".images").addClass("mousehover").stop().animate({
	//		"height":"160px"															  
	//	},500).find("img").stop().animate({
	//		"margin-top":"-30px"	
	//	},500);	
	//	$(this).find(".view").stop().animate({
	//		"bottom":"0"									 
	//	},500);
		
	//},function(){
	//	$(this).find(".hoverline").fadeOut().end().find(".images").removeClass("mousehover").stop().animate({
	//		"height":"222px"															  
	//	},500).find("img").stop().animate({
	//		"margin-top":"0"	
	//	},500);	
	//	$(this).find(".view").stop().animate({
	//		"bottom":"-60px"									 
	//	},500);
		
	//});
	
});

