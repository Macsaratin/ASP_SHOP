<template>
    <div class="container py-5">
      <div class="row">
        <div class="col-md-4">
          <img
            :src="movie.posterUrl"
            class="img-fluid rounded"
            alt="Poster"
            style="object-fit: cover; height: 100%;"
          />
        </div>
        <div class="col-md-8">
          <h2>{{ movie.title }}</h2>
          <p class="lead">{{ movie.description }}</p>
          <p><strong>🎬 Thể loại:</strong> {{ movie.genre }}</p>
          <p><strong>📅 Khởi chiếu:</strong> {{ formatDate(movie.releaseDate) }}</p>
          <p><strong>🕒 Thời gian:</strong> {{ movie.durationMinutes }} phút</p>
          <p><strong>⭐ Đánh giá:</strong> {{ movie.rating }}/10</p>
          <p><strong>🌍 Quốc gia:</strong> {{ movie.country }}</p>
          <div v-if="movie.trailerUrl">
            <h4>🎥 Trailer</h4>
            <iframe
              :src="movie.trailerUrl"
              width="100%"
              height="315"
              frameborder="0"
              allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture"
              allowfullscreen
            ></iframe>
          </div>
          <button class="btn btn-primary mt-4" @click="goBack">Quay lại</button>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, onMounted } from 'vue';
  import { useRoute, useRouter } from 'vue-router';
  import ProductService from '../../../service/productService';
  
  const router = useRouter();
  const route = useRoute();
  
  const movie = ref(null);
  
  // Fetch movie details by ID
  const fetchMovieDetail = async (movieId) => {
    try {
      movie.value = await ProductService.getMovieById(movieId);
    } catch (error) {
      console.error('Error fetching movie details:', error);
    }
  };
  
  // Go back to the previous page
  const goBack = () => {
    router.push('/Movies'); // Assuming '/movies' is the list of movies page
  };
  
  // Format date to a readable format
  const formatDate = (dateStr) => new Date(dateStr).toLocaleDateString('vi-VN');
  
  // Fetch movie details on component mount
  onMounted(() => {
    const movieId = route.params.id;
    fetchMovieDetail(movieId);
  });
  </script>
  
  <style scoped>
  .container {
    max-width: 1200px;
  }
  img {
    max-height: 400px;
    object-fit: cover;
    border-radius: 8px;
  }
  </style>
  