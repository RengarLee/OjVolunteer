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
		    if(! /^.{6,18}$/.test(value)){
		    	return '登录帐号必须在6到18位之间';
		    }
		  }
		  ,pass: [
		    /^[\S]{6,18}$/
		    ,'密码必须在6到18位之间，且不能出现空格'
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
          ,actionName:[
		  	/^[\S]{5,20}$/
		  	,'活动名称只能在5-20个字之间,且不能出现空格'
            ]
          ,joinNumber:[
		  	/^[0-9]*[1-9][0-9]*$/
		  	,'人数上上限不能为负数'
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


