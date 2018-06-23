layui.use(['form','layer'],function(){
		var form = layui.form;
		form.verify({
		  	userID: function(value, item){ //value：表单的值、item：表单的DOM对象
		    if(!new RegExp("^[a-zA-Z0-9_s·]+$").test(value)){
		      return '登录帐号不能有特殊字符和中文字符';
		    }
		    if(/(^\_)|(\__)|(\_+$)/.test(value)){
		      return '登录帐号首尾不能出现下划线\'_\'';
		    }
		    if(/^\d+\d+\d$/.test(value)){
		      return '登录帐号不能全为数字';
		    }
		    if(! /^.{6,12}$/.test(value)){
		    	return '登录帐号必须在6到12位之间';
		    }
		  }
		  ,pass: [
		    /^[A-Za-z0-9]{6,12}$/
		    ,'密码必须为数字或字符并在6到12位之间'
		  ]
		  ,repass:function(value){
		  	var passvalue = document.getElementById("userpass").value;
		  	if(value != passvalue){
		  		return '两次密码输入不一致';
		  	}
		  }
		  ,orgrepass:function(value){
		  	var orgpassvalue = document.getElementById("orgpass").value;
		  	if(value != orgpassvalue){
		  		return '两次密码输入不一致';
		  	}
		  }
		  ,username:[
		   	/^[\u4e00-\u9fa5]{2,8}$/
		   	,'姓名必须在2到8个汉字之间'
		  ]
		  ,orgname: [
            /^[\u4e00-\u9fa5]{2,20}$/
            , '团队名称必须在2到20个汉字之间'
          ]
		  ,studentNumber:[
		  	/^[0-9]{11}$/
		  	,'学号必须位11位，且都是数字'
            ]
            ,actionName: function (value) {
                if (Number(value) > 20 || Number(value) < 2) {
                    return '活动名称必须在2-20个字符之间';
                }
            }

          ,joinNumber:[
		  	/^[0-9]*[1-9][0-9]*$/
		  	,'人数上限不能为负数'
            ]
           ,moreAddress:[
		  	/^[\S]{0,50}$/
		  	,'详细地址不能超过50个字'
            ]
          ,MaxNumber:function(value){
		  	if(Number(value ) > 99){
		  		return '人数上限最多只为99人';
		  	}
		  }
		});      
});

function checkPass(index) {
    $(index).blur(function () {
        var reg = /^[0-9]{11}$/;
        if ($(this).val() != reg) {
            alert("错误");
        }
        else
            alert('正确');
    });
}
$(function(){
	layui.use('layer', function() {
 		var layer = layui.layer;
 });
	checkPass('#phonenum');
})


