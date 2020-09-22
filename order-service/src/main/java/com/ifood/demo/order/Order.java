package com.ifood.demo.order;

import java.util.Date;
import java.util.List;
import java.util.UUID;

import javax.persistence.Id;

import com.fasterxml.jackson.databind.annotation.JsonSerialize;

import org.springframework.data.mongodb.core.mapping.Document;

import lombok.Data;
import lombok.RequiredArgsConstructor;

@Data
@Document
@RequiredArgsConstructor
@JsonSerialize
public class Order {

	private @Id UUID id = UUID.randomUUID();
	private final UUID clientId;
	private final UUID restaurantId;
	private final Date createdAt;
	private final Date confirmedAt;
	private final List<Item> items;

	@Data
	@RequiredArgsConstructor
	@JsonSerialize
	public static class Item {

		private final String description;
		private final Integer quantity;
		private final Double price;

	}

}
