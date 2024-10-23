async function convertHtmlToPdfString() {  
        // 将 HTML 内容转换为 PDF  
    let worker = html2pdf().from(document.body); 
    return new Promise(async (resolve, reject) => {
        try {
            //const TextNode = document.createTextNode("正在转化PDF");
            const p = document.createElement("p");
            p.style.position = "fixed";
            p.style.textAlign = "center";
            p.style.paddingTop = "10%";
            p.style.fontWeight = "bold";
            p.style.fontSize = "larger"; 
            p.id = "PDF_Title";
            p.innerHTML = "正在转化PDF";
            document.body.appendChild(p); 
            let pdfBlob = await worker.output('dataurlstring');
            
            resolve(pdfBlob);
        }
        catch (e) {
            reject(e); 
        }
    })
}  
function getPdfString() {
    if (window.globalPdf) {
        let pdfString = window.globalPdf; 
        let node = document.getElementById("PDF_Title"); 
        if (node) {
            document.body.removeChild(node);
        }
        console.log(pdfString); 
        window.globalPdf = false; 
        return pdfString; 
    }
    convertHtmlToPdfString().then((pdfString) => {
        window.globalPdf = pdfString;
        let node = document.getElementById("PDF_Title"); 
        if (node) {
            node.innerHTML = "已完成PDF转化，再次点击Export以分享PDF"; 
        }
    })
    return "{}"; 
}

async function blob2base64(blob) {
    let reader = new FileReader();
    reader.readAsDataURL(blob);
    reader.onload = function (e) {
        return reader.result;
    };
}
function arrayBufferToString(arrayBuffer) {
    const uint8Array = new Uint8Array(arrayBuffer);
    const decoder = new TextDecoder();
    return decoder.decode(uint8Array);
}  