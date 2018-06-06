//function survey(adText) {
//    document.getElementById("content2").style.visibility = "visible";
//    document.getElementById("Survey-Question").innerHTML = adText;
//}
//function onClickAnswer(answer) {
//    // collect question and answer
//    var surveyQuestion = document.getElementById("Survey-Question");
//    var questionID = surveyQuestion.attributes.getNamedItem("value");

//    clearInterval(myVar);
//    document.getElementById("header").innerHTML = "Your Wifi is ready!";
//    createButon();
//    // ajax and Q&A in pair to controller [questionID, answer]

//}



function onClickAnswer(answer) {
    // collect question and answer surveyID: {11}
    var surveyQuestion = document.getElementById("Survey-Question");
    var questionID = surveyQuestion.attributes.getNamedItem("value");
    // ajax and Q&A in pair to controller [questionID, answer]
	
	
	
	
}
function summitAnswer(id, value){
    $.ajax({
        url: '@Url.Action("SummitAnswer", "Landing")',
        data: { questionId : id, answer : value},
        type: "POST",
        success: function (result) {
            document.getElementById("summitSuccess").innerHTML  = "Thank you. You can connect!";

        },
        error: function (error) {
            alert("@Resources.Message_EN_Resources.ErrorOccured");
        }
    });
}
