
//  header Swiper
const swiper = new Swiper('.header_swiper', {
    // Optional parameters
    direction: 'horizontal',
    loop: true,
    autoplay: {
        delay: 1500,
    },
    effect: "fade",
    speed: 3000
});






const swiper2 = new Swiper('.category_swiper', {
    // Optional parameters
    direction: 'horizontal',
    loop: true,
 
    // Autoplay
    autoplay: {
        delay: 1500,
     },

    // Slides display
    slidesPerView: 3.5,
    spaceBetween: 15, 
    speed: 2000,
    freeMode: true,
    freeModeMomentum: true,
});

