$(document).ready(function(){




    // 调用layui弹出层
    layui.use('layer', function(){
    var layer = layui.layer;
    }); 

    // 点赞功能
    dianZan();
    // 点击加载更多功能
    addMore();
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
// 点击加载更多
function addMore(){
    $('.addMore').find('p').click(function(){
        layer.load(1);
        //此处演示关闭
        setTimeout(function(){
          layer.closeAll('loading');
        }, 800);
    })
}
