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
		  //数组的两个值分别代表：[正则匹配、匹配不符时的提示文字]
		  ,pass: [
		    /^[\S]{6,12}$/
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
		   	/^[\u4e00-\u9fa5]{2,10}$/
		   	,'姓名必须在2到10个汉字之间'
		  ]
		  ,orgname: [
            /^[\u4e00-\u9fa5]{2,12}$/
            , '组织名称必须在2到12个汉字之间'
          ]
		  ,studentNumber:[
		  	/^[0-9]{11}$/
		  	,'学号必须位11位，且都是数字'
		  ]
		});      
});


