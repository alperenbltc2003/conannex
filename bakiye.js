var inputtl=document.querySelector("#tlhot");
var inputusd=document.querySelector("#usdhot");
var butonhesap=document.querySelector("#butonhot");
butonhesap.addEventListener("click",function(e){    
    let sonuc=inputtl.value*67410.27;
    inputusd.value=sonuc;
});