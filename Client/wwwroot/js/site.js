window.fadeIn = (id) => {
    const el = document.getElementById(id);
    if (el) {
        el.style.opacity = 0;
        el.style.transition = "opacity 1s ease-in-out";
        setTimeout(() => {
            el.style.opacity = 1;
        }, 100);
    }
};
