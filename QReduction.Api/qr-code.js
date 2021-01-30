module.exports = function (callback,guid) {
    console.log(guid);
    const QRCode = require('easyqrcodejs-nodejs');
   const options = {
       text: guid,
       width: 256,
       height: 256
    };
    const qrcode = new QRCode(options);

     qrcode.toDataURL().then(data => {
        console.info('======QRCode PNG Base64 DataURL======')
        console.info(data);
        callback(/* error */ null, data);
    }).catch(function (e) {
        callback(/* error */ e, null);
    });  
};