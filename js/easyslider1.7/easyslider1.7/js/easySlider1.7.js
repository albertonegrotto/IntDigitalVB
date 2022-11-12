/*
 * 	Easy Slider 1.7 - jQuery plugin
 *	written by Alen Grakalic	
 *	http://cssglobe.com/post/4004/easy-slider-15-the-easiest-jquery-plugin-for-sliding
 *
 *	Copyright (c) 2009 Alen Grakalic (http://cssglobe.com)
 *	Dual licensed under the MIT (MIT-LICENSE.txt)
 *	and GPL (GPL-LICENSE.txt) licenses.
 *
 *	Built for jQuery library
 *	http://jquery.com
 *
 */
 
/*
 *	markup example for $("#slider").easySlider();
 *	
 * 	<div id="slider">
 *		<ul>
 *			<li><img src="images/01.jpg" alt="" /></li>
 *			<li><img src="images/02.jpg" alt="" /></li>
 *			<li><img src="images/03.jpg" alt="" /></li>
 *			<li><img src="images/04.jpg" alt="" /></li>
 *			<li><img src="images/05.jpg" alt="" /></li>
 *		</ul>
 *	</div>
 *
 */

(function($) {

	$.fn.easySlider = function(options){
	  
		// default configuration properties
		var defaults = {			
			prevId: 		'prevBtn',
			prevText: 		'Previous',
			nextId: 		'nextBtn',	
			nextText: 		'Next',
			controlsShow:	false,
			controlsBefore:	'',
			controlsAfter:	'',	
			controlsFade:	true,
			firstId: 		'firstBtn',
			firstText: 		'First',
			firstShow:		false,
			lastId: 		'lastBtn',	
			lastText: 		'Last',
			lastShow:		false,				
			vertical:		false,
			speed: 			800,
			auto:			false,
			pause:			5000,
			continuous:		false, 
			numeric: 		false,
			numericId: 		'controls',
			incremento: 	0
		}; 
		
		var options = $.extend(defaults, options);  
				
		this.each(function() {  
			var continuar = true;
			var obj = $(this); 				
			var s = $("li", obj).length;
			var w = $("li", obj).width(); 
			var h = $("li", obj).height(); 
			var timeout;
			var clickable = true;
			obj.width(w); 
			obj.height(h); 
			obj.css("overflow","hidden");
			var ts = s-1;
			var t = 0;
			$("ul", obj).css('width',s*w);	

			function prueba(){
				alert('prueba');
			};


			function setCurrent(i){
				i = parseInt(i)+1;
				$("li", "#" + options.numericId).removeClass("current");
				$("li#" + options.numericId + i).addClass("current");
			};
			
			function adjust(){
				if(t>ts) t=0;		
				if(t<0) t=ts;	
				if(!options.vertical) {
					$("ul",obj).css("margin-left",(t*w*-1));
				} else {
					$("ul",obj).css("margin-left",(t*h*-1));
				}
				clickable = true;
				if(options.numeric) setCurrent(t);
			};
			
			function animate(dir,clicked){
				if(continuar){
					if (clickable){
						clickable = false;
						var ot = t;				
						switch(dir){
							case "next":
								t = (ot>=ts) ? (options.continuous ? t+1 : ts) : t+1;						
								break; 
							case "prev":
								t = (t<=0) ? (options.continuous ? t-1 : 0) : t-1;
								break; 
							case "first":
								t = 0;
								break; 
							case "last":
								t = ts;
								break; 
							default:
								t = dir;
								break; 
						};	
						var diff = Math.abs(ot-t);
						var speed = diff*options.speed;						
						if(!options.vertical) {
							//p = (t*w*-1);
							p = (t*options.incremento*-1);
							$("ul",obj).animate(
								{ marginLeft: p }, 
								{ queue:false, duration:options.speed, complete:adjust }
							);				
						} 
						else {
							p = (t*h*-1);
							$("ul",obj).animate(
								{ marginTop: p }, 
								{ queue:false, duration:speed, complete:adjust }
							);	
						};
						
						if(!options.continuous && options.controlsFade){					
							if(t==ts){
								$("a","#"+options.nextId).hide();
								$("a","#"+options.lastId).hide();
							} else {
								$("a","#"+options.nextId).show();
								$("a","#"+options.lastId).show();					
							};
							if(t==0){
								$("a","#"+options.prevId).hide();
								$("a","#"+options.firstId).hide();
							} else {
								$("a","#"+options.prevId).show();
								$("a","#"+options.firstId).show();
							};					
						};				
						
						if(clicked) clearTimeout(timeout);
						if(options.auto && dir=="next" && !clicked){;
							timeout = setTimeout(function(){
								animate("next",false);
							},options.pause);
						};
					};
				};	
			};		
			
			$(this).mouseenter(function(){
				continuar = false;
			});

			$(this).mouseleave(function(){
				continuar = true;
				clearTimeout(timeout);
				timeout = setTimeout(function(){
					animate("next",false);
				},options.pause);
			});			
			if(options.continuous){
				$("ul", obj).prepend($("ul li:last-child", obj).clone().css("margin-left","-"+ w +"px"));
				$("ul", obj).append($("ul li:nth-child(2)", obj).clone());
				$("ul", obj).css('width',(s+1)*w);
			};				
			
			if(!options.vertical) $("li", obj).css('float','left');
						
			// init
			//var timeout;
			if(options.auto){;
				timeout = setTimeout(function(){
					animate("next",false);
				},options.pause);
			};		


			
		});
	  
	};

})(jQuery);



