
"use strict";
let Pic;
let mobilenet;
let Results=[];
let floating=["shoe","wood","tree","sandal","bottle"];
let NonFloating= ["metal", "nail","coin","gong,","tam","shield,"];
let NotRelated = ["cat","Kitty","dog", "rabbit"]
let TheResult;
let TheProp;
var ResultWordsArray;
var isFloat;
let theimg;
let position;

document.getElementById('BtnSink').disabled = 'true';

function dragNdrop(event) {
    var fileName = URL.createObjectURL(event.target.files[0]);
    var preview = document.getElementById("preview");
    var previewImg = document.createElement("img");
    previewImg.setAttribute("src", fileName);
    preview.innerHTML = "";
    preview.appendChild(previewImg);
    Pic = previewImg;
}
function drag() {
    document.getElementById('uploadFile').parentNode.className = 'draging dragBox';
}
function drop() {
    document.getElementById('uploadFile').parentNode.className = 'dragBox';
}
function dooo() {
    console.log('ml5 version:', ml5.version);
    go();
}
function modelReady() {
    console.log('Model Is Ready');
    mobilenet.predict(Pic , gotResults);
}
function gotResults(error , results)
{
    if (error) {
        console.error(error);
    }
    else {
        console.log(results);
        console.log(results[0].label);
        Results = results;
        TheResult = results[0].label;
        TheProp = results[0].confidence;
        ResultWordsArray = TheResult.trim().split(" ");
        console.log(ResultWordsArray);
        console.log(ResultWordsArray.length);
        
        CheckIt();

    }
}
var BDC = 400; 
function CheckIt() {
    if ( TheProp*100 > 50) {
document.getElementById('displayDetection').innerHTML = "I'd Say It's A " + TheResult;
    }
    if ( TheProp*100 < 50) {
        document.getElementById('displayDetection').innerHTML = "Hmmm, is it A  " + TheResult + "? I'm not sure !";
    }
    DiscoverFloat();
}

function go() {
mobilenet = ml5.imageClassifier('MobileNet', modelReady);
return mobilenet;
}
function Detect() {
    var button = document.getElementById('BtnSink');
    button.disabled = false;
    document.getElementById('displayDetection').innerHTML = "Checking ... Please Wait "
    go();
    position = document.getElementById("preview").getBoundingClientRect();
    console.log(position);
}
function Sink() {
    theimg = document.getElementById("preview");
    console.log(isFloat);
    if ( isFloat == 1 ) {
        anime({
            targets: theimg,
            translateY: 125,
            duration: 2000,
            rotate: '7deg'
          });
          document.getElementById('displayDetection').innerHTML = "I Think it will Float";
        }
    else if ( isFloat == 0 ){
        anime({
              targets: theimg,
              translateY: BDC,
              duration: 6000,
              rotate: '120deg'
            });
            document.getElementById('displayDetection').innerHTML = "I Think it will Sink";
        }
        else if ( isFloat == 2 ){
                document.getElementById('displayDetection').innerHTML = "oh, So Cute! but I won't drop it into water";
            }
            else {
                document.getElementById('displayDetection').innerHTML = "I'm Sorry! I Can't Decide";
            }
}

function DiscoverFloat() {
    if ( NonFloating.includes( ResultWordsArray[0] ) || NonFloating.includes(ResultWordsArray[0] ) ) {
        isFloat = 0;
    }
    else if ( floating.includes(ResultWordsArray[0]) || floating.includes(ResultWordsArray[1])) {
        isFloat = 1;
    }
    else if ( NotRelated.includes(ResultWordsArray[0]) || NotRelated.includes(ResultWordsArray[1]) ) {
        isFloat = 2;
    }
    else {
        isFloat = 3;
    }
}


// // Function definiton with passing two arrays 
// async function IsThatFloating(ResultWordsArray, floating) { 
//      await  gotResults();
//     // Loop for array1 
//     for(let i = 0; i < ResultWordsArray.length ; i++) { 
          
//         // Loop for array2 
//         for(let j = 0; j < floating.length ; j++) { 
              
//             // Compare the element of each and 
//             // every element from both of the 
//             // arrays 
//             if(ResultWordsArray[i] === floating[j]) { 
              
//                 // Return if common element found 
//                 return true; 
//                 isFloat = 1;
//             } 
//         } 
//     } 
      
//     // Return if no common element exist 
//     return false;  
//     isFloat = 0;
// }

// async function IsThatNonFloating(ResultWordsArray, NonFloating) { 
//     await  gotResults();
//    // Loop for array1 
//    for(let i = 0; i < ResultWordsArray.length ; i++) { 
         
//        // Loop for array2 
//        for(let j = 0; j < NonFloating.length ; j++) { 
             
//            // Compare the element of each and 
//            // every element from both of the 
//            // arrays 
//            if(ResultWordsArray[i] === NonFloating[j]) { 
             
//                // Return if common element found 
//                return true; 
//            } 
//        } 
//    } 
     
//    // Return if no common element exist 
//    return false;  
// }

function Reset() {
    window.location.reload();
}