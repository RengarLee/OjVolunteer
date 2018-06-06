$(document).ready(function(){

    var topback = $('#topback');
    // 返回顶部按钮点击事件
    topback.on("click",function(){
        $('html,body').animate({
        	scrollTop:0
        },200)

    })
    $(window).on('scroll',function(){
    	if($(window).scrollTop()>200)
    		topback.fadeIn();
    	else
    		topback.fadeOut();
    })
    $(window).trigger('scroll');


    // 调用layui弹出层
    layui.use('layer', function(){
    var layer = layui.layer;
    }); 

    // 点赞功能
    dianZan();
    // 点击加载更多功能
    addMore();
// 	演示所用(后删)
    hidediv();
})

 // 点赞功能
function dianZan(){
    $('.zan').click(function(){
        if($(this).hasClass('yiZan')){
            layer.msg('只能点赞一次哦~');
        }
        else{
            $(this).addClass('yiZan');    
            layer.msg('点赞成功！'); 
            var zanNums = Number($(this).find('span').text());
            zanNums++;
            $(this).find('span').text(zanNums);    
        }

    })
}
// 点击加载更多(演示)
function addMore(){
    $('.addMore').find('p').click(function(){
        $('#showajax').show();
    })
}
function hidediv(){
	$('#showajax').hide();
}
