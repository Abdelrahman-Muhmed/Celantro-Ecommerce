

//Start Slide
document.addEventListener("DOMContentLoaded", function () {
    const track = document.querySelector('.carousel-track');
    const slides = Array.from(track.children);
    const nextButton = document.getElementById('nextBtn');
    const prevButton = document.getElementById('prevBtn');
    const slideWidth = slides[0].getBoundingClientRect().width;
    let currentIndex = 0;

    // Position slides next to each other
    const setSlidePosition = (slide, index) => {
        slide.style.left = `${slideWidth * index}px`;
    };

    slides.forEach(setSlidePosition);

    // Move to the next slide
    const moveToSlide = (track, currentSlide, targetSlide) => {
        track.style.transform = 'translateX(-' + targetSlide.style.left + ')';
        currentIndex = slides.indexOf(targetSlide);
    };

    // Next button functionality
    nextButton.addEventListener('click', () => {
        const currentSlide = slides[currentIndex];
        const nextSlide = currentIndex === slides.length - 1 ? slides[0] : slides[currentIndex + 1];
        moveToSlide(track, currentSlide, nextSlide);
    });

    // Previous button functionality
    prevButton.addEventListener('click', () => {
        const currentSlide = slides[currentIndex];
        const prevSlide = currentIndex === 0 ? slides[slides.length - 1] : slides[currentIndex - 1];
        moveToSlide(track, currentSlide, prevSlide);
    });

    // Automatic slide every 30 seconds
    setInterval(() => {
        const currentSlide = slides[currentIndex];
        const nextSlide = currentIndex === slides.length - 1 ? slides[0] : slides[currentIndex + 1];
        moveToSlide(track, currentSlide, nextSlide);
    }, 30000); // 30000 milliseconds = 30 seconds
});
//End Slide 