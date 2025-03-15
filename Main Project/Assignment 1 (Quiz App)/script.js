var startQuiz = document.getElementById("startQuiz");
var showQuestions = document.getElementById("showQuestions");
var Result = document.getElementById("Result");
var quesText = document.getElementById("quesText");
var Selections = document.getElementById("Selections");
var NextBtn = document.getElementById("NextBtn");
var totalScore = document.getElementById("totalScore");
var ResetBtn = document.getElementById("ResetBtn");
var score = 0;

$("#startQuiz").click(function () {

    $("#startQuiz").hide();
    $("#showQuestions").show();
    showQues();
});

var questions =
    [
        {
            question: "What is the capital of United Kingdom?",
            choices: ["Manchester", "Birmingham", "London", "Birmingham"],
            answer: "London"
        },

        {
            question: "Which character was not there in the Film The Angry Birds?",
            choices: ["Red", "Chuck", "Pumba", "Bomb"],
            answer: "Pumba"
        },

        {
            question: "What is the stick used to play a violin called?",
            choices: ["Stick", "Bow", "Row", "Tie"],
            answer: "Bow"
        },

        {
            question: "What is the capital of United States?",
            choices: ["California", "New York", "Miami", "Florida"],
            answer: "New York"
        },

        {
            question: "How many ballon d'or does messi have?",
            choices: ["5", "6", "7", "8"],
            answer: "8"
        }

    ];
var questionIndex = 0;
function showQues() {
    hideNextBtn();
    var question = questions[questionIndex];
    var shownQuestion = question.question;
    quesText.innerText = shownQuestion;
    question.choices.forEach(option => {
        const btn = document.createElement("button");
        btn.innerText = option;
        // btn.innerHTML = "<br>"
        // btn.classList.add("otherBtn-decoration");
        btn.classList.add("otherBtn-decoration");
        btn.addEventListener("click", () => selectAnswer(btn, question.answer));
        Selections.appendChild(btn);
    });
}

function hideNextBtn() {
    NextBtn.classList.add("hide");
    quesText.innerHTML = "";
    Selections.innerHTML = "";
}

function selectAnswer(btn, CurrentQueAns) {
    if (btn.innerText === CurrentQueAns) 
    {
        btn.classList.add("success");
        score++;
    }
    else 
    {
        btn.classList.add("failure");
    }
    document.querySelectorAll(".otherBtn-decoration").forEach(
        btn => btn.disabled = true
    );
    NextBtn.classList.remove("hide");
}

NextBtn.addEventListener("click", () => NextQues());
ResetBtn.addEventListener("click", () => ResetQuiz());

function NextQues() {
    if(questionIndex < 4)
    {
        questionIndex++;
        showQues();
    }
    else
    {
        ResultOfQuiz();
    }
}

function ResultOfQuiz() {
    quesText.innerHTML = "";
    Selections.innerHTML = "";
    NextBtn.classList.add("hide");
    // showQuestion.classList.add("hide");
    ResetBtn.classList.remove("hide");
    Result.classList.remove("hide");
    totalScore.innerText = `${score} / ${questions.length}`;
}

function ResetQuiz() {
    
    totalScore.innerText = "";
    $("#startQuiz").show();
    ResetBtn.classList.add("hide");
    questionIndex = 0;
}
