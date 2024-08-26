using System;

class CallbackTimer {
    public float timer;
    private Action<float> callback;

    public CallbackTimer(float timer, Action<float> callback){
        this.timer = timer;
        this.callback = callback;
    }
    
    public Action<float> GetCallback() {
        return callback;
    }
}