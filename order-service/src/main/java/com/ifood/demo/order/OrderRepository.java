package com.ifood.demo.order;

import java.util.Collection;
import java.util.Date;
import java.util.UUID;

import org.springframework.data.repository.CrudRepository;
import org.springframework.data.repository.query.Param;
import org.springframework.data.rest.core.annotation.RestResource;
import org.springframework.format.annotation.DateTimeFormat;

public interface OrderRepository extends CrudRepository<Order, UUID> {

	@RestResource(path = "byClientId")
	Collection<Order> findByClientId(@Param("clientId") UUID clientId);
	
	@RestResource(path = "byRestaurantId")
	Collection<Order> findByRestaurantId(@Param("restaurantId") UUID restaurantId);
	
	@RestResource(path = "byDate")
	Collection<Order> findByCreatedAtBetween(
		@Param("start")
		@DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME)
		Date start,

		@Param("end")
		@DateTimeFormat(iso = DateTimeFormat.ISO.DATE_TIME)
		Date end
	);
}