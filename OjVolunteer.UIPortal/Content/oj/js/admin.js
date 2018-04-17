// 弹出层js
var layer;
layui.use('layer',function(){
	var layer = layui.layer;
});

//点击基本资料弹出表单页面
$('#admininfo').on('click', function () {
    var index = layer.open({
        type: 2,
        title: false,
        closeBtn: 0,
        maxmin: false,
        shadeClose: true, //点击遮罩关闭层
        area: ['600px', '700px'],
        content: '/OrganizeInfo/GetSelf'
    });
});
//点击确认键关闭弹出层 	
    $('#success').on('click', function(){
	    	
	});
//	点击退出关闭弹出层
	$('#cancel').on('click', function(){
	    	var index = parent.layer.getFrameIndex(window.name);
	    	parent.layer.close(index);
	});
	




 
//表单页JS
layui.use(['form', 'layedit', 'laydate'], function(){
  var form = layui.form
  ,layer = layui.layer
  ,layedit = layui.layedit
  ,laydate = layui.laydate;
  
  //日期
  laydate.render({
    elem: '#date'
  });
  laydate.render({
    elem: '#date1'
  });
  
  //创建一个编辑器
  var editIndex = layedit.build('LAY_demo_editor');
 
  //自定义验证规则
  form.verify({
    title: function(value){
      if(value.length < 5){
        return '标题至少得5个字符啊';
      }
    }
    ,pass: [/(.+){6,12}$/, '密码必须6到12位']
    ,content: function(value){
      layedit.sync(editIndex);
    }
  });
  
  //监听提交
  form.on('submit(demo1)', function(data){
    layer.alert(JSON.stringify(data.field), {
      title: '最终的提交信息'
    })
    return false;
  });
  
  
});

//头像上传
layui.use('upload', function(){
  var $ = layui.jquery
  ,upload = layui.upload;
  
  //普通图片上传
  var uploadInst = upload.render({
    elem: '#test1'
    ,url: '/upload/'
    ,before: function(obj){
      //预读本地文件示例，不支持ie8
      obj.preview(function(index, file, result){
        $('#demo1').attr('src', result); //图片链接（base64）
      });
    }
    ,done: function(res){
      //如果上传失败
      if(res.code > 0){
        return layer.msg('上传失败');
      }
      //上传成功
    }
    ,error: function(){
      //演示失败状态，并实现重传
      demoText.find('.demo-reload').on('click', function(){
        uploadInst.upload();
      });
    }
  });
  
  

  
});