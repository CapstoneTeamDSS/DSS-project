function QuizClock(controlId, totalSec, callback) {
    this.setSec = totalSec;
    this.controlID = controlId;
    this.callback = callback;
}

QuizClock.prototype.start = function () {
    this.currSec = this.setSec;
    this.count();
}

QuizClock.prototype.display = function () {
    return this.currSec;
}

QuizClock.prototype.count = function () {
    this.currSec--;
    $(this.controlID).html(this.display());

    if (this.currSec == 0) {
        this.callback();
    } else {
        var me = this;
        setTimeout(function () {
            me.count();
        }, 1000);
    }
}

QuizClock.prototype.endClock = function() {
    this.currSec = 0;
    this.count();
}

QuizClock.prototype.reset = function (totalSec) {
    this.setSec = totalSec;
    return this;
}
